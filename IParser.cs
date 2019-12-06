using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Searcher
{
    public interface IParser
    {
        String searchEngineUrl
        {
            get;
        }
        String xPathUrl
        {
            get;
            set;
        }
        List<KeyValuePair<String, String>> parameters 
        { 
            get;
        }
        Task LoadPage(string searchingText);
        List<String> Parse();
    }
}
