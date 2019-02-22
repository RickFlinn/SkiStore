using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SkiStore.Data;
using SkiStore.Models;

namespace SkiStore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder().AddEnvironmentVariables();
            builder.AddUserSecrets<Startup>();

            Configuration = builder.Build();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        { 
            services.AddMvc();

            services.AddIdentity<SkiStoreUser, IdentityRole>()
                    .AddEntityFrameworkStores<SkiStoreUserDbContext>()
                    .AddDefaultTokenProviders();

            services.AddDbContext<SkiStoreUserDbContext>(options =>
                     options.UseSqlServer(Configuration["ConnectionStrings:DefaultUserDbConnection"]));

            services.AddDbContext<SkiStoreProductDbContext>(options =>
                     options.UseSqlServer(Configuration["ConnectionStrings:DefaultDbConnection"]));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();

          
        }
    }
}
