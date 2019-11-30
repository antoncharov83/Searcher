using System;
using System.Collections.Generic;

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
        void LoadPage(string searchingText);
        List<String> Parse();
    }
}
