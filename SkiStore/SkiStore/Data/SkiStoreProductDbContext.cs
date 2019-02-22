using Microsoft.EntityFrameworkCore;
using SkiStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkiStore.Data
{
    public class SkiStoreProductDbContext : DbContext
    {
        public SkiStoreProductDbContext(DbContextOptions<SkiStoreProductDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
        }

        public DbSet<Product> Products { get; set; }
    }
}
