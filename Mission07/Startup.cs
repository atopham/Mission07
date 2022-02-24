using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mission07.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission07
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<BookContext>(options =>
            {
                //options.UseSqlite(Configuration["ConnectionStrings:BookDBConnection"]);
                options.UseSqlite(Configuration.GetConnectionString("BookDBConnection"));
            });
            services.AddScoped<IBookRepository, EFBookRepository>();
            services.AddRazorPages();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // by default, the endpoint is in order name, pattern, default
                endpoints.MapControllerRoute("categorypage",
                    // naming "Category" in HomeController and in Default
                     "{category}/Page{pageNum}",
                     new { Controller = "Home", action = "Index" });

                // endpoints are run in order. The first endpoint here is the one that doesn't explicitly say "pageNum" in the URL
                endpoints.MapControllerRoute(name: "Paging", pattern: "{pageNum}", defaults: new { Controller = "Home", action = "Index", pageNum = 1 });

                endpoints.MapControllerRoute("category",
                    "{category}",
                    new { Controller = "Home", action = "Index", pageNum = 1 });

                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();


            });
        }
    }
}
