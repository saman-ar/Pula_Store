using Data.AppContext;
using Entities.AppIdentities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
//using Data.Dtos;

namespace Data.Repo
{
	public class UserRepository
	{
		private readonly AppDbContext _context;
		private readonly UserManager<AppUser> _userManager;
		
		public UserRepository(AppDbContext context,UserManager<AppUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}
		public List<AppUser> GetAll()
		{
			return _context.Users.ToList();
		}

		public AppUser FindByName(string name)
		{
			var result=_context.Users.Where(u => u.UserName == name).SingleOrDefault();
			return result;
		}

		public AppUser FindById(string id)
		{
			throw new NotImplementedException();
		}

		public AppUser GetClaims()
		{
			throw new NotImplementedException();
		}


        
    }
}
