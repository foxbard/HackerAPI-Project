using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hackerAPI.Client.Models
{
    public class ItemsParams
    {
        public int PageNumber { get; set; } = 1;
        public int PageSizeMax { get; set; } = 200;
        public int PageSizeMin = 100;
        private int _PageSize = 100;
        public int PageSize
        {
            get
            {
                return _PageSize;
            }
            set
            {
                _PageSize = (value > PageSizeMax) ? PageSizeMax : value;
            }
        } 
        
        
        public string Next { get; set; }
        public string Prev { get; set; }
    }
}
