using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Searcher
{
    public class Preferences
    {
        public List<IParser> parsers { get; }
        public int count { get; set; }

        public Preferences() {
            count = 10;
            parsers = new List<IParser>();
        }

        public void addEngine(IParser parser) {
            parsers.Add(parser);
        }
    }
}
