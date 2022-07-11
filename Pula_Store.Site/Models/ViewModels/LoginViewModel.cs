using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Site.Models.ViewModels
{
	public class LoginViewModel
	{
		[Required(ErrorMessage ="*")]
		[Display(Name ="نام کاربری")]
		[MaxLength(100, ErrorMessage = "*")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "*")]
		[DataType(DataType.Password)]
		[Display(Name ="رمز عبور")]
		[MaxLength(100,ErrorMessage ="*")]
		public string Password { get; set; }

		[Display(Name = "مرا بخاطر بسپار?")]
		public bool RememberMe { get; set; }
	}

}
