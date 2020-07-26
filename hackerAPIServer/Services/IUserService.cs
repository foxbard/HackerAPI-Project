using hackerAPI.Client.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hackerAPI.Client.Services
{
    public interface IUserService
    {
        Task<User> getAsyncUserByName (string name);
        Task<List<int>> GetAsyncUserSubmittedItems(string name);
    }
}
