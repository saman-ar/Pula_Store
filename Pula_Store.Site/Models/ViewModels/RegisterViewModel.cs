using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Site.Models.ViewModel
{
	public class RegisterViewModel
	{
		
		[Required]
		//[EmailAddress]
		[Display(Name = "نام کاربری")]
		public string UserName { get; set; }

		[Required]
		[EmailAddress]
		[Display(Name = "ایمیل")]
		public string Email { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
		[DataType(DataType.Password)]
		[Display(Name = "رمز عبور")]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "تکرار رمز عبور")]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }
	}
}
