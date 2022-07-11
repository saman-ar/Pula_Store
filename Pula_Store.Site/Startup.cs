using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.AppContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Entities.AppIdentities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Design;
using WebFramework.Extensions;
using AutoMapper;
using System.Reflection;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Entities.Products;

namespace Pula_Store.Site
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
            ///adding AutoMapper
            services.AddAutoMapper(Assembly.Load("Site")/*typeof(AppSrv.MapperProfiles)*/);

            ///injecting DbContext
            services.AddDbContext<AppDbContext>(opts => opts.UseSqlServer(Configuration.GetConnectionString("default")));

            ///defining Migration Assembly
            //services.AddDbContext<AppDbContext>(opts => opts.UseSqlServer(Configuration.GetConnectionString("default"), b => b.MigrationsAssembly("Data")));

            ///===> injecting DbContext with custome Excution Strateghy
            //services.AddDbContext<AppDbContext>(opts => opts.UseSqlServer(Configuration.GetConnectionString("default"),optsProvider=> 
            //	optsProvider.EnableRetryOnFailure
            //		(
            //			maxRetryCount:10,
            //			maxRetryDelay : TimeSpan.FromSeconds(20),
            //			null
            //		)));


            //adding App Repository
            services.AddApplicationServices();

            ///define new passwordOption 
            PasswordOptions passwordOptions = new PasswordOptions
            {
                RequireDigit = false,
                RequiredLength = 3,
                RequireNonAlphanumeric = false,
                RequireUppercase = false,
                RequireLowercase = false
            };

            ///injecting and setting Identity Core
            services.AddIdentity<AppUser, IdentityRole>(opts =>
                {
                    opts.Password = passwordOptions;
                })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            //adding custome claims
            services.AddCustomeClaimsFactory();

            ///disable client-side validation entire App
            //services.AddMvc().AddViewOptions(opts => opts.HtmlHelperOptions.ClientValidationEnabled = false).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                //options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            #region test

            //services.AddTransient<ICategory, Category>(serviceProvider =>
            // {
            //     var os = new Category(serviceProvider.GetService(typeof(Category),);
            //     return null;
            // });
            #endregion

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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            //add Auth middelware
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                         name: "default",
                         template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                         name: "areas",
                         template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
