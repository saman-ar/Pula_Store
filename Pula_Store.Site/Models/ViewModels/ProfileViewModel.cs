using Entities.AppIdentities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Site.Models.ViewModels
{
	public class ProfileShowViewModel
	{
		//[BindNever]
		public string Id { get; set; }

		[Display(Name ="نام")]
		public string FirstName { get; set; }

		[Display(Name = "نام خانوادگی")]
		public string LastName { get; set; }

		[Display(Name = "نام کاربری")]
		public string UserName { get; set; }

		[Display(Name ="ایمیل")]
		public string Email { get; set; }

		[Display(Name = "آدرس")]
		public Address Address { get; set; }

		[Display(Name = "شماره تلفن")]
		public string PhoneNumber { get; set; }


	}
}
