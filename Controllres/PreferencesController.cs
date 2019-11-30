using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;

namespace Searcher.Controllres
{
    public class PreferencesController : Controller
    {
        private readonly Preferences preferences;
        public PreferencesController(Preferences preferences)
        {
            this.preferences = preferences;
        }

        public IActionResult Index()
        {
            return View(preferences);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult addEngine(String url, String xPath)
        {
            try
            {
                if (!Uri.IsWellFormedUriString(url, UriKind.Absolute)){
                    ViewBag.messageUri = "Некорректный url";
                }
                XPathExpression.Compile(xPath);
                preferences.parsers.Add(new Parser(url, xPath, preferences));
            }
            catch (XPathException e)
            {
                ViewBag.messageXPath = "Некорректное выражение xPath - " + e.Message;
            }
            return View("Index", preferences);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult changeXPath(String selectUrl, String xPath)
        {
            try
            {              
                XPathExpression.Compile(xPath);
                preferences.parsers.First(p => p.searchEngineUrl.Equals(selectUrl)).xPathUrl = xPath;
            }
            catch (XPathException e)
            {
                ViewBag.messageXPath = "Некорректное выражение xPath - " + e.Message;
            }
            return View("Index", preferences);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult addKeyValue(String selectUrl,String key, String value)
        {
            preferences.parsers.First(p => p.searchEngineUrl.Equals(selectUrl)).parameters.Add(new KeyValuePair<String, String>(key, value));
            return RedirectToAction("Index");
        }
    }
}