using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net;
using System.Xml.XPath;

namespace Searcher
{
    public class Parser : IParser
    {
        public String searchEngineUrl { get; set; }
        string xPathUrl;//= "//div[@class='serp-list']//span[@class='serp-url__item']//a[1]"
        String content;
        HtmlDocument doc;
        public List<KeyValuePair<String, String>> parameters { get; }
        Preferences preferences { get; }

        public Parser(String searchEngineUrl, string xPathUrl, Preferences preferences) {
            this.searchEngineUrl = searchEngineUrl;
            //try
            //{
            //    XPathExpression expr = XPathExpression.Compile(xPathUrl);
            //}
            //catch (XPathException e)
            //{

            //}
            this.xPathUrl = xPathUrl;
            parameters = new List<KeyValuePair<String, String>>();
            doc = new HtmlDocument();
            this.preferences = preferences;
        }

        public void addParameter(String name, String value) {
            parameters.Add(new KeyValuePair<String, String>(name, value));
        }

        public void deleteParameter(String name) {
            parameters.Remove(parameters.First(p => p.Key.ToLower().Equals(name.ToLower())));
        }

        public void changeParameter(String name, String value) {
            deleteParameter(name);
            addParameter(name, value);
        }

        public void LoadPage(string searchingText)
        {
            WebClient client = new WebClient();
            client.Headers.Add("user-agent", "Mozilla/5.0 (compatible; U; ABrowse 0.6; Syllable) AppleWebKit/420+ (KHTML, like Gecko)");
            String urlAdress = searchEngineUrl + searchingText;
            parameters.ForEach(p => urlAdress += "&" + p.Key + "=" + p.Value);
            content = client.DownloadString(urlAdress);
        }

        public List<String> Parse() {
            if (content == null)
                return new List<String>();
            doc.LoadHtml(content);

            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(xPathUrl);

            List<String> result = new List<String>();
            if (nodes != null)
            {
                foreach (HtmlNode node in nodes.Take(preferences.count))
                {
                    result.Add(node.InnerText);
                }
            }
            return result;
        }
    }
}
