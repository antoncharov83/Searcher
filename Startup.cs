﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Searcher.DAL;

namespace Searcher
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            var preferences = new Preferences();
            preferences.addEngine(new Parser(@"http://yandex.ru/yandsearch?text=", "//div[@class='serp-list']//span[@class='serp-url__item']//a[1]", preferences));
            preferences.addEngine(new Parser(@"https://www.google.ru/search?q=", "//div/div[1]/a/div[2]", preferences));
            preferences.addEngine(new Parser(@"https://search.yahoo.com/search?p=", "//ol/li/div/div[1]/div/span[1]", preferences));
            services.AddSingleton(preferences);

            string connection = Configuration.GetConnectionString("DefaultConnection");
            string connectionDocker = Configuration.GetConnectionString("DockerConnection");
            services.AddDbContext<UrlContext>(options =>
                options.UseSqlServer(connection));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Main}/{action=Index}/{searchingString?}");
            });
        }
    }
}
