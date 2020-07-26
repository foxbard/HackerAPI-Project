using EasyCaching.Core;
using hackerAPI.Client.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
        
        public CacheService(IItemService itemService, IUserService userService, IEasyCachingProviderFactory cachingProviderFactory)
        {
            _itemService = itemService;
            _userService = userService;
            this.cachingProviderFactory = cachingProviderFactory;
            this.cachingProvider = this.cachingProviderFactory.GetCachingProvider("redisCache");

        }

        
        public ActionResult ProcessRedisCache(string keyValue, string category)
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

        public ActionResult StoreItemsInRedisCache(string keyValue)
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
                        }
                        catch(AggregateException err)
                        {
                            throw err;
                            
                        }
                        
                    }

                    var converted = JsonConvert.SerializeObject(itemsList);

                    this.cachingProvider.Set(keyValue, converted, TimeSpan.FromSeconds(100));
                    var returnData = this.cachingProvider.Get<string>(keyValue);
                    if (returnData != null)
                    {
                        return Ok(returnData.Value);
                    }
                    else
                    {
                        return NotFound();
                    }

                }
                return Ok(this.cachingProvider.Get<string>(keyValue).Value);
            }
       // }
            
            
            

    }
}
