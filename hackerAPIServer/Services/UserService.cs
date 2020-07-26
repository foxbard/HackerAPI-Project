using hackerAPI.Client.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace hackerAPI.Client.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _url = "https://hacker-news.firebaseio.com/v0";

        public UserService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        
        #region Async Tasks

        public async Task<User> getAsyncUserByName(string name)
        {
            var client = _clientFactory.CreateClient();
            var userData = await client.GetFromJsonAsync<User>($"{_url}/user/{name.ToLower()}.json");
            return userData;
        }
        public async Task<List<int>> GetAsyncUserSubmittedItems(string name)
        {
            var client = _clientFactory.CreateClient();
            var userItems = await client.GetFromJsonAsync<User>($"{_url}/user/{name.ToLower()}.json");
            return userItems.submitted;
        }

        #endregion
    }
}
