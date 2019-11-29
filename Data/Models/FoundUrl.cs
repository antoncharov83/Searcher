using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Searcher.Data.Models
{
    public class FoundUrl
    {
        public int ID { get; set; }
        public string Url { get; set; }
        public string Engine { get; set; }
        public DateTime DateFound { get; set; }
    }
}
