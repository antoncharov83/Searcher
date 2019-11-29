using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Searcher
{
    public interface IParser
    {
        String searchEngineUrl
        {
            get;
        }
        List<KeyValuePair<String, String>> parameters 
        { 
            get;
        }
        void LoadPage(string searchingText);
        List<String> Parse();
    }
}
