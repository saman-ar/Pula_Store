using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.AppIdentities
{
	public class AppUser : IdentityUser
	{
		[MaxLength(100,ErrorMessage ="*")]
		public string FirstName { get; set; }

		[MaxLength(150, ErrorMessage = "*")]
		public string LastName { get; set; }

		public Address Address { get; set; }

		public bool IsRemoved { get; set; }

		public bool IsActive { get; set; }

	}
}

