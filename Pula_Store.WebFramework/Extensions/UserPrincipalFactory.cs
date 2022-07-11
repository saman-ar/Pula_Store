using Entities.AppIdentities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebFramework.Extensions
{
	public class CustomClaimsPrincipalFactory : UserClaimsPrincipalFactory<AppUser, IdentityRole>
	{
		public CustomClaimsPrincipalFactory(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager,
																  IOptions<IdentityOptions> optionsAccessor)
			 : base(userManager, roleManager, optionsAccessor)
		{
		}

		public async override Task<ClaimsPrincipal> CreateAsync(AppUser user)
		{
			var principal = await base.CreateAsync(user);

			// Add your claims here
			//((ClaimsIdentity)principal.Identity).AddClaims(new[] { new Claim(ClaimTypes.NameIdentifier, user.Id),
			//																		 new Claim(ClaimTypes.Email, user.Email),
			//																		 new Claim("FirstName", user.FirstName),
			//																		 new Claim("LastName", user.LastName)
			//																	 });

			var claimsIdentity = new ClaimsIdentity();
			//claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
			////claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
			//claimsIdentity.AddClaim(new Claim(ClaimTypes.GivenName, string.IsNullOrEmpty(user.FirstName)? "":user.FirstName));
			//claimsIdentity.AddClaim(new Claim(ClaimTypes.Surname, string.IsNullOrEmpty(user.LastName) ? "" : user.LastName));

			claimsIdentity.AddClaims(new Claim[]
			{  //new Claim(ClaimTypes.NameIdentifier, user.Id),
				new Claim(ClaimTypes.Email, user.Email),
				new Claim(ClaimTypes.GivenName, string.IsNullOrEmpty(user.FirstName)? "":user.FirstName),
				new Claim(ClaimTypes.Surname , string.IsNullOrEmpty(user.LastName) ? "" : user.LastName)
			});

			principal.AddIdentity(claimsIdentity);
			return principal;
		}
	}
}
