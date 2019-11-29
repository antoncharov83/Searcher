using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            preferences.parsers.Add(new Parser(url, xPath, preferences));
            return RedirectToAction("Index");
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