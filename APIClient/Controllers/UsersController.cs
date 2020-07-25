using System.Collections.Generic;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using hackerAPI.Client.models;
using hackerAPI.Client.Interfaces;

namespace hackerAPI.Client.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController: ControllerBase
    {
        private readonly string _url = "https://hacker-news.firebaseio.com/v0";

        private readonly IHttpClientFactory _clientFactory;
        public UsersController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            
        }

        [HttpGet("getUserByName/{name}")]
        public ActionResult<User> getUserByName(string name){
            var data = Task.FromResult(getAsyncUser(name)).Result.Result;
            return Ok(data);
        }

        [HttpGet("getUserItemsByName/{name}")]
        public ActionResult <User> getUserItemsByName(string name){
            var data = Task.FromResult(GetAsyncUserSubmittedItems(name));
            return Ok(data);
        }


    #region Async Tasks

        private async Task<User> getAsyncUser(string name){
            var client = _clientFactory.CreateClient();
            var userData = await client.GetFromJsonAsync<User>($"{_url}/user/{name.ToLower()}.json");
            return userData;
        }
        private async Task<List<int>> GetAsyncUserSubmittedItems(string name){
            var client = _clientFactory.CreateClient();
            var userItems = await client.GetFromJsonAsync<User>($"{_url}/user/{name.ToLower()}.json");
            return userItems.submitted;
        }
        
    #endregion
    }
}