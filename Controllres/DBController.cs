using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Searcher.DAL;
using Searcher.Data.Models;

namespace Searcher.Controllres
{
    public class DBController : Controller
    {
        private readonly UrlContext context;

        public DBController(UrlContext context) {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(String searchingText)
        {
            List<FoundUrl> urls = context.urls.Where(p => p.Url.Contains(searchingText)).ToList();
            return View(urls);
        }
    }
}