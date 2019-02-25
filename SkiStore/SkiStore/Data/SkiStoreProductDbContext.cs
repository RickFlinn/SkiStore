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
            // Establish composite keys
            builder.Entity<CartItem>().HasKey(ci => new { ci.UserID, ci.ProductID });

            //Seed database
            builder.Entity<Product>().HasData(
               new Product
               {
                   ID = 1,
                   Name = "Skis",
                   Price = 300
               },
               new Product
               {
                   ID = 2,
                   Name = "Helmet",
                   Price = 150
               },
               new Product
               {
                   ID = 3,
                   Name = "Goggles",
                   Price = 60
               },
               new Product
               {
                   ID = 4,
                   Name = "Poles",
                   Price = 100
               },
               new Product
               {
                   ID = 5,
                   Name = "Boots",
                   Price = 200
               },
               new Product
               {
                   ID = 6,
                   Name = "Jacket",
                   Price = 100
               },
               new Product
               {
                   ID = 7,
                   Name = "Pants",
                   Price = 100
               },
               new Product
               {
                   ID = 8,
                   Name = "Gloves",
                   Price = 40
               },
               new Product
               {
                   ID = 9,
                   Name = "Flask",
                   Price = 20
               },
               new Product
               {
                   ID = 10,
                   Name = "Buff",
                   Price = 40
               });
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
    }
}
