using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using hackerAPI.Client.Interfaces;
using Microsoft.AspNetCore.Mvc;
using hackerAPI.Client.models;

namespace hackerAPI.Client.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController: ControllerBase
    {
        
        private readonly string _url = "https://hacker-news.firebaseio.com/v0";

        private readonly IHttpClientFactory _clientFactory;
        public ItemsController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
           
        }

        [HttpGet("getNewStories")]    
        public ActionResult <List<int>> getNewStories(){
            var data = Task.FromResult(getAsyncStories("newstories")).Result;
            
            if(data != null){
                return Ok(data.Result);
            }else {
                return NotFound();
            }
        }

        [HttpGet("getTopStories")]    
        public ActionResult <List<int>> getToptories(){
            var data = Task.FromResult(getAsyncStories("topstories")).Result;
            
            if(data != null){
                return Ok(data.Result);
            }else {
                return NotFound();
            }
        }

        [HttpGet("getBestStories")]    
        public ActionResult <List<int>> getBestStories(){
            var data = Task.FromResult(getAsyncStories("beststories")).Result;
            
            if(data != null){
                return Ok(data.Result);
            }else {
                return NotFound();
            }
        }

        
        [HttpGet("getStoryById/{id}")]
        public ActionResult <Item> getNewestStoryById(int id){
            var data = Task.FromResult(getAsyncStoryById(id)).Result;
            if(data != null){
                return Ok(data.Result);
            }else {
                return NotFound();
            }
        }




        #region Async Tasks
        
            private async Task<List<int>> getAsyncStories(string category){
                var client = _clientFactory.CreateClient();
                client.DefaultRequestHeaders.Add("Accept-Content","application/json");
                var data = await client.GetFromJsonAsync<List<int>>($"{_url}/{category}.json");
                return data.ToList();
            }

            // Get Item by ID and return single Item;
            private async Task<Item> getAsyncStoryById(int id){
                var client = _clientFactory.CreateClient();
                var resultItem = await client.GetFromJsonAsync<Item>($"{_url}/item/{id}.json");
                return resultItem;
            }

        #endregion
    }
}