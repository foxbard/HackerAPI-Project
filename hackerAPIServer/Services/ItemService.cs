using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using hackerAPI.Client.models;

namespace hackerAPI.Client.Services
{
    public class ItemService: IItemService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _url = "https://hacker-news.firebaseio.com/v0";

        public ItemService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        #region Async Tasks


        public async Task<List<int>> GetAsyncStories(string category)
        {
            var client = _clientFactory.CreateClient();
            var url = $"{_url}/{category}.json";
            //client.DefaultRequestHeaders.Add("Accept-Content", "application/json");
            var data = await client.GetFromJsonAsync<List<int>>(url);
            return data;
        }

        // Get Item by ID and return single Item;
        public async Task<Item> GetAsyncStoryById(int id)
        {
            var client = _clientFactory.CreateClient();
            var resultItem = await client.GetFromJsonAsync<Item>($"{_url}/item/{id}.json");
            return resultItem;
        }
        #endregion
    }
}
