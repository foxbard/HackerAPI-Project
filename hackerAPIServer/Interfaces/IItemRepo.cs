using System.Collections.Generic;
using System.Threading.Tasks;
using hackerAPI.Client.models;
using hackerAPI.Client.Models;
using Microsoft.AspNetCore.Mvc;

namespace hackerAPI.Client.Interfaces {
    public interface IItemRepo{

        IActionResult getItemsFromCachedByCategory(string category);
        List<int> GetNewStories(ItemsParams itemsParams);
        List<int> GetTopStories(ItemsParams itemsParams);
        List<int> GetBestStories(ItemsParams itemsParams);

        Item GetNewsestStoryById(int id);

       
    }
}