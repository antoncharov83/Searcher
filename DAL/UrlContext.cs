using Microsoft.EntityFrameworkCore;
using Searcher.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Searcher.DAL
{
    public class UrlContext : DbContext
    {
        public DbSet<FoundUrl> urls { get; set; }
        public UrlContext(DbContextOptions<UrlContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FoundUrl>(entity => entity.HasKey(e => e.ID));
        }
    }
}
