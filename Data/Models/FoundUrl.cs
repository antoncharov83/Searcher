using System;

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
