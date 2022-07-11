using Entities.AppIdentities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Site.Models.ViewModels
{
	public class EditViewModel
	{
		[Required]
		[BindRequired]
		public string UserId { get; set; }

		[BindRequired]
		[Required]
		public string FullName { get; set; }

		[Required]
		[BindRequired]
		public string PhoneNumber { get; set; }
		
		//public Address Address { get; set; }
	}
}
