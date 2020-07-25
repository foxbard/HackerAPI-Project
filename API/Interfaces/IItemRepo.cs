using System.Collections.Generic;
using System.Threading.Tasks;
using hackerAPI.Client.models;

namespace hackerAPI.Client.Interfaces {
    public interface IItemRepo{

        List<int> getNewStories();
        List<int> getTopStories();
        List<int> getBestStories();

        Item getNewsestStoryById(int id);

        Task<List<int>> getAsyncStories(int id);

        Task<List<Item>> getAsyncStoryById(int id);
    }
}