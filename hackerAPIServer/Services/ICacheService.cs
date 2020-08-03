using hackerAPI.Client.models;
using hackerAPI.Client.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hackerAPI.Client.Services
{
    public interface ICacheService
    {
        IEnumerable<Item> GetItemsFromCache(string keyValue);
        Task<IEnumerable<Item>> StoreItemsInRedisCache(string keyValue, ItemsParams itemsParams);
        IActionResult ProcessRedisCache(string name, string v);
    }
}
