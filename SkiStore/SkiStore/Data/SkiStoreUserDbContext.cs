using Microsoft.EntityFrameworkCore;
using SkiStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkiStore.Data
{
    public class SkiStoreUserDbContext : DbContext
    {

        public SkiStoreUserDbContext(DbContextOptions<SkiStoreUserDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<SkiStoreUser>().HasData(
                new SkiStoreUser
                {
                    UserName = "SkiGuy",
                    FirstName = "Skier",
                    LastName = "Skimanovski",

                    DateOfBirth = new DateTime(1),
                    AgreedToWaiver = true.ToString()
                });
        }

        public DbSet<SkiStoreUser> SkiStoreUsers { get; set; }

    }
}

