using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hackerAPI.Client.Services
{
    public interface ICacheService
    {

        ActionResult StoreItemsInRedisCache(string key);
        ActionResult ProcessRedisCache(string name, string v);
    }
}
