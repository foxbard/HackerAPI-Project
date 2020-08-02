using System.Collections.Generic;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using hackerAPI.Client.models;
using hackerAPI.Client.Interfaces;
using hackerAPI.Client.Services;
using EasyCaching.Core;

namespace hackerAPI.Client.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController: ControllerBase
    {
      

        private readonly IUserService _userService;
        private readonly ICacheService _cacheService;

        public UsersController(IUserService userService, ICacheService cacheService)
        {
            _userService = userService;
            _cacheService = cacheService;

           

        }

        [HttpGet("getUserByName/{name}")]
        public IActionResult getUserByName(string name){
            var data = Task.FromResult(_userService.getAsyncUserByName(name)).Result;
            return Ok(data.Result);
        }

        [HttpGet("getUserItemsByName/{name}")]
        public IActionResult getUserItemsByName(string name){
            var results = _cacheService.ProcessRedisCache(name, "user");
            return results;
        }


    
    }
}