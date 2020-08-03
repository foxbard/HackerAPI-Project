using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using hackerAPI.Client.Interfaces;
using Microsoft.AspNetCore.Mvc;
using hackerAPI.Client.models;
using hackerAPI.Client.Services;
using System.Runtime.CompilerServices;
using EasyCaching.Core;
using hackerAPI.Client.Models;

namespace hackerAPI.Client.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {

       
        private readonly IItemService _itemService;
        private readonly ICacheService _cacheService;
        
        public ItemsController(IItemService itemService, ICacheService cacheService)
        {
            _itemService = itemService;
            _cacheService = cacheService;

        }


        
        
        [HttpGet("getNewStoriesIds")]
        public async Task<IActionResult> GetNewStoriesNoCache()
        {
            var results = await _itemService.GetAsyncStories("newstories");
            if(results != null)
            {
                return Ok(new ResponseService<List<int>>(results));
            }
            else
            {
                return NotFound();
            }
            
        }

        // Returns all story items
        [HttpGet("getItemsCachedByCategory/{category}")]
        public IActionResult getItemsFromCachedByCategory(string category)
        {
            var results =  _cacheService.GetItemsFromCache(category).OrderBy(items => items.id);
            if(results != null)
            {
                return Ok(results);
            } else
            {
                return NotFound();
            }
            
        }

        [HttpGet("getNewStories")]
        public async Task<IActionResult> GetNewStoriesAsync([FromQuery] ItemsParams itemsParams)
        {
            // caching in redis
            var results = await _cacheService.StoreItemsInRedisCache("newstories", itemsParams);
            if (results != null)
            {
                return Ok(results);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpGet("getTopStories")]
        public async Task<IActionResult> GetTopStoriesAsync([FromQuery] ItemsParams itemsParams)
        {
            // caching in redis
            var results = await _cacheService.StoreItemsInRedisCache("topstories", itemsParams);
            if (results != null)
            {
                return Ok(results);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpGet("getBestStories")]
        public async Task<IActionResult> GetBestStoriesAsync([FromQuery] ItemsParams itemsParams)
        {
            // caching in redis
            var results = await _cacheService.StoreItemsInRedisCache("beststories", itemsParams);
            if (results != null)
            {
                return Ok(results);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpGet("getStoryById/{id}")]
        public IActionResult GetNewestStoryByIdAsync(int id)
        {
            var data = Task.FromResult(_itemService.GetAsyncStoryById(id)).Result;
            if (data != null)
            {
                return Ok(data.Result);
            }
            else
            {
                return NotFound();
            }
        }



    }
}