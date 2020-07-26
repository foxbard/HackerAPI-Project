using hackerAPI.Client.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hackerAPI.Client.Services
{
    public interface IItemService
    {
        Task<List<int>> GetAsyncStories(string category);

        Task<Item> GetAsyncStoryById(int id);
    }
}
