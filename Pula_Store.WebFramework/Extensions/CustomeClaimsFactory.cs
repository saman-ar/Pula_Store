using Entities.AppIdentities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebFramework.Extensions
{
	public static class CustomeClaimsFactory
	{
		public static IServiceCollection AddCustomeClaimsFactory(this IServiceCollection services)
		{
			services.AddScoped<IUserClaimsPrincipalFactory<AppUser>, CustomClaimsPrincipalFactory>();
			return services;
		}
	}
}
