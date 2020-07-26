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

        // used for first load of stories, does not cache at this call.
        [HttpGet("getNewStoriesIds")]
        public ActionResult<List<int>> GetNewStoriesNoCache()
        {
            var results = Task.FromResult(_itemService.GetAsyncStories("newstories")).Result;
            return Ok(results.Result);
        }

        [HttpGet("getNewStories")]
        public ActionResult<List<int>> GetNewStories()
        {
            // caching in redis
            var results = _cacheService.StoreItemsInRedisCache("newstories");
            return results;

        }

        [HttpGet("getTopStories")]
        public ActionResult<List<int>> GetTopStories()
        {
            // caching in redis
            var results = _cacheService.StoreItemsInRedisCache("topstories");
            return results;

        }

        [HttpGet("getBestStories")]
        public ActionResult<List<int>> GetBestStories()
        {
            // caching in redis
            var results = _cacheService.StoreItemsInRedisCache("beststories");
            return results;
        }


        [HttpGet("getStoryById/{id}")]
        public ActionResult<Item> GetNewestStoryById(int id)
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