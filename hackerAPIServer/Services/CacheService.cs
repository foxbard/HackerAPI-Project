using AutoMapper.QueryableExtensions;
using EasyCaching.Core;
using hackerAPI.Client.models;
using hackerAPI.Client.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace hackerAPI.Client.Services
{
    public class CacheService : ControllerBase, ICacheService
    {

        private IEasyCachingProvider cachingProvider;
        private IEasyCachingProviderFactory cachingProviderFactory;
        private readonly IItemService _itemService;
        private readonly IUserService _userService;
        private readonly int timeAlive = 500;

        public CacheService(IItemService itemService, IUserService userService, IEasyCachingProviderFactory cachingProviderFactory)
        {
            _itemService = itemService;
            _userService = userService;
            this.cachingProviderFactory = cachingProviderFactory;
            this.cachingProvider = this.cachingProviderFactory.GetCachingProvider("redisCache");

        }

        //Get all Items from redis cache with keyValue
        public IEnumerable<Item> GetItemsFromCache(string keyValue)
        {
            var itemsList = new List<Item>();
            if (this.cachingProvider.Exists(keyValue))
            {
                itemsList = JsonConvert.DeserializeObject<List<Item>>(this.cachingProvider.Get<string>(keyValue).Value);
                return itemsList.OrderBy(item => item.id);
                
            }
            return itemsList;
        }

        public async Task<IEnumerable<Item>> StoreItemsAsync(string keyValue, ItemsParams itemsParams, int? totalItems = null)
        {
            var itemsList = new List<Item>();
            // this.cachingProvider.Flush();

            var items = await Task.FromResult(_itemService.GetAsyncStories(keyValue)).Result;

            foreach (var item in items)
            {
                var i = Task.FromResult(_itemService.GetAsyncStoryById(item));
                itemsList.Add(i.Result.Result);
                var converted = JsonConvert.SerializeObject(itemsList);
                this.cachingProvider.Set(keyValue, converted, TimeSpan.FromSeconds(timeAlive));
                if(itemsList.Count >= itemsParams.PageSize && totalItems == null)
                {
                    itemsList.OrderBy(item => item.id);
                    break;
                }
            }
            return itemsList;
        }


        public IActionResult ProcessRedisCache(string keyValue, string category)
        {


            if (!this.cachingProvider.Exists(keyValue))
            {


                if (category == "user")
                {
                    var ukeyValue = "user_" + keyValue;
                    var data = Task.FromResult(_userService.GetAsyncUserSubmittedItems(keyValue)).Result;
                    this.cachingProvider.Set(ukeyValue, data.Result, TimeSpan.FromSeconds(500));
                    var returnData = this.cachingProvider.Get<List<int>>(ukeyValue);
                    if (returnData != null)
                    {
                        return Ok(returnData.Value);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    var data = Task.FromResult(_itemService.GetAsyncStories(keyValue)).Result;
                    this.cachingProvider.Set(keyValue, data.Result, TimeSpan.FromSeconds(500));
                    var returnData = this.cachingProvider.Get<List<int>>(keyValue);
                    if (returnData != null)
                    {
                        return Ok(returnData.Value);
                    }
                    else
                    {
                        return NotFound();
                    }
                }



            }
            else
            {
                if (category == "user")
                {
                    return Ok(this.cachingProvider.Get<List<int>>("user_" + keyValue).Value);
                }
                else
                {
                    return Ok(this.cachingProvider.Get<List<int>>(keyValue).Value);
                }

            }
        }

        public async Task<IEnumerable<Item>> StoreItemsInRedisCache(string keyValue, ItemsParams itemsParams)
        {


            _ = StoreItemsAsync(keyValue, itemsParams, 500);
            var itemsList = await StoreItemsAsync(keyValue, itemsParams);

            var pages = JsonConvert.DeserializeObject<List<Item>>
                            (this.cachingProvider.Get<string>(keyValue).Value);

            var paged = pages
                        .OrderBy(item => item.id)
                        .Skip((itemsParams.PageNumber - 1) * itemsParams.PageSize)
                        .Take(itemsParams.PageSize)
                        .ToList();
            return paged;

        }
    }
}
