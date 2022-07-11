using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Data.AppContext;
using Data.Repo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Services.ProductForAdmin;
using Services.ProductForSite;

namespace WebFramework.Extensions
{
	public static class ServiceCollectionExtension
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddScoped<CategoryRepo>();

			services.AddScoped<ProductForAdminRepo>();
			services.AddScoped<ProductsForAdminServices>();

			services.AddScoped<ProductsForSiteServices>();
            services.AddScoped<ProductForSiteRepo>();

            return services;
		}
	}
}
