using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Searcher.DAL;

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
            return View(context.urls.Select(p => p.Url.Contains(searchingText)));
        }
    }
}