using System.Collections.Generic;
namespace hackerAPI.Client.models {
    public class User{

        public string id {get; set;}
        public int delay {get; set;}

        public int created {get; set;}

        public int number { get; set; }
        public string about { get; set; }
        public List<int> submitted { get; set; }
    }

}


