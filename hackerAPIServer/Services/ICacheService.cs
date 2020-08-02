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

        IActionResult StoreItemsInRedisCache(string key, ItemsParams itemsParams);
        IActionResult ProcessRedisCache(string name, string v);
    }
}
