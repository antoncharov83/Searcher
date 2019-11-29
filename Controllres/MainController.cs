using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Searcher.DAL;
using Searcher.Data.Models;

namespace Searcher.Controllres
{
    public class MainController : Controller
    {
        private readonly Preferences preferences;
        private readonly UrlContext context;

        public MainController(Preferences preferences, UrlContext context) {
            this.preferences = preferences;
            this.context = context;
        }

        public ViewResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ViewResult Index(String searchingText)
        {        
            try
            {
                Task[] tasks = new Task[preferences.parsers.Count];
                int i = 0;
                preferences.parsers.ForEach(p => { tasks[i] = Task.Run(() => p.LoadPage(searchingText)); i++; });
                int index = Task.WaitAny(tasks);

                List<String> result = preferences.parsers[index].Parse();

                foreach (var r in result)
                {
                    FoundUrl url = new FoundUrl();
                    url.Url = r;
                    url.Engine = preferences.parsers[index].searchEngineUrl;
                    url.DateFound = DateTime.Now;
                    context.Add(url);
                }
                context.SaveChanges();

                return View(result);
            }
            catch (AggregateException ex)
            {
                List<String> result = new List<String>();
                foreach (var e in ex.InnerExceptions)
                {
                    result.Add(e.Message);                  
                }
                return View(result);
            }
        }
    }
}