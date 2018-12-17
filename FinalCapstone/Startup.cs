using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FinalCapstone.Controllers;
using FinalCapstone.Dal;

namespace FinalCapstone
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddScoped<IFoodItemDAL>(x => new FoodItemSqlDAL(Configuration.GetConnectionString("MacroGo")));
            services.AddScoped<IRestaurantDAL>(x => new RestaurantSqlDAL(Configuration.GetConnectionString("MacroGo")));
            services.AddScoped<IUserDAL>(x => new UserSqlDAL(Configuration.GetConnectionString("MacroGo")));
            services.AddScoped<IUserFavoritesSqlDAL>(x => new UserFavoritesSqlDAL(Configuration.GetConnectionString("MacroGo")));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDistributedMemoryCache();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            
            app.UseSession();  


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
