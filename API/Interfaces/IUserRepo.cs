using hackerAPI.Client.models;
using System.Collections.Generic;

namespace hackerAPI.Client.Interfaces
{
    public interface IUserRepo
    {
        User getUserByName(string name);
        List<int> getUserItemsByName(string name);
    }
}