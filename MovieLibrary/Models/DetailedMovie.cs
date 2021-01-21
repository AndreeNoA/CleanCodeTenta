using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieLibrary.Models
{
    public class DetailedMovie
    {
        public string id { get; set; }
        public string title { get; set; }
        public string rated { get; set; }
        public int[] ratings { get; set; }
    }
}
