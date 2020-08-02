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
        public IActionResult GetNewStoriesNoCache()
        {
            var results = Task.FromResult(_itemService.GetAsyncStories("newstories")).Result;
            return Ok(new ResponseService<List<int>>(results.Result));
        }

        [HttpGet("getNewStories")]
        public IActionResult GetNewStories([FromQuery] ItemsParams ItemsParams)
        {
            // caching in redis
            var results = _cacheService.StoreItemsInRedisCache("newstories", ItemsParams);
            return results;

        }

        [HttpGet("getTopStories")]
        public IActionResult GetTopStories([FromQuery] ItemsParams ItemsParams)
        {
            // caching in redis
            var results = _cacheService.StoreItemsInRedisCache("topstories", ItemsParams);
            return results;

        }

        [HttpGet("getBestStories")]
        public IActionResult GetBestStories([FromQuery] ItemsParams ItemsParams)
        {
            // caching in redis
            var results = _cacheService.StoreItemsInRedisCache("beststories", ItemsParams);
            return results;
        }


        [HttpGet("getStoryById/{id}")]
        public IActionResult GetNewestStoryById(int id)
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