using System.Collections.Generic;

namespace hackerAPI.Client.models {
    public class Item {
    public int id { get; set; }
    public bool deleted { get; set; }
    public string type { get; set; }
    public string by { get; set; }
    public int  time { get; set; }
    public string text { get; set; }
    public bool dead { get; set; }
    public List<int> poll { get; set; }
    public List<int> kids { get; set; }
    public string url {get; set;}
    public int score { get; set; }
    public string title { get; set; }
    public List<int> parts { get; set; }
    public List<int> descendents {get; set;}
}
}
