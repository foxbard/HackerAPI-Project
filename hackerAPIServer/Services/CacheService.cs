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

       /* public async Task StoreItems(string keyValue, List<Item> data)
        {
           var task = await this.cachingProvider.SetAsync<List<Item>>(keyValue, data, TimeSpan.FromSeconds(timeAlive));
            return;
            
        }*/
        public IActionResult ProcessRedisCache(string keyValue, string category)
        {

            
            if (!this.cachingProvider.Exists(keyValue))
            {
                
                
                if(category == "user")
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
                    return Ok(this.cachingProvider.Get<List<int>>("user_"+keyValue).Value);
                }
                else
                {
                    return Ok(this.cachingProvider.Get<List<int>>(keyValue).Value);
                }
                
            }
        }

        public IActionResult StoreItemsInRedisCache(string keyValue, ItemsParams itemsParams)
        {
            var itemsList = new List<Item>();


                if (!this.cachingProvider.Exists(keyValue))
                {
                    var data = Task.FromResult(_itemService.GetAsyncStories(keyValue)).Result.Result;

                    foreach (var d in data)
                    {
                        try
                        {
                            itemsList.Add(Task.FromResult(_itemService.GetAsyncStoryById(d)).Result.Result);
                        if (itemsList.Count > itemsParams.PageSize)
                        {
                            // cache full data in background
                            var converted = JsonConvert.SerializeObject(itemsList);
                            this.cachingProvider.RemoveAsync(keyValue).Wait();
                            this.cachingProvider.SetAsync(keyValue, converted, TimeSpan.FromSeconds(timeAlive));

                        }
                        else if (itemsList.Count == itemsParams.PageSize)
                                {
                                    // cache and paginate ItemsParams.pageSizeMax
                                    var page = itemsList
                                        .OrderBy(item => item.id)
                                        .Skip((itemsParams.PageNumber - 1) * itemsParams.PageSize)
                                        .Take(itemsParams.PageSize)
                                        .ToList();

                                    var converted = JsonConvert.SerializeObject(page);

                                this.cachingProvider.SetAsync(keyValue, converted, TimeSpan.FromSeconds(timeAlive));
                                    var returnData = this.cachingProvider.Get<string>(keyValue);
                                    if (returnData != null)
                                    {
                                        var items = itemsList;
                                    
                                        return Ok(page);
                                    }
                                    else
                                    {
                                        return NotFound();
                                    }
                            } 
                        }
                        catch(AggregateException err)
                        {
                            throw err;
                            
                        }
                        
                    }





            }
            else
            {
                var value = JsonConvert.DeserializeObject<List<Item>>(this.cachingProvider.Get<string>(keyValue).Value);
                var paginatedData = value
                                    .OrderBy(item => item.id)
                                    .Skip((itemsParams.PageNumber - 1) * itemsParams.PageSize)
                                    .Take(itemsParams.PageSize)
                                    .ToList();

                return Ok(paginatedData);
            }
            return Ok();
            }
 
    }
}
