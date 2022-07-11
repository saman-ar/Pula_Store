using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Entities.AppIdentities
{
	public class Address
	{
		public int Id { get; set; }

		[Required]
		[BindRequired]
		[Display(Name="استان")]
		[MaxLength(50,ErrorMessage = "حداکثر 50 کارکتر میتواند باشد")]
		public string Ostan { get; set; }


		[Required]
		[BindRequired]
		[Display(Name = "شهر")]
		[MaxLength(50, ErrorMessage = "حداکثر 50 کارکتر میتواند باشد")]
		public string City { get; set; }


		[Required]
		[BindRequired]
		[Display(Name = "ادرس کامل")]
		[MaxLength(50, ErrorMessage = "حداکثر 250 کاراکتر میتواند باشد")]
		[DataType(DataType.MultilineText)]
		public string FullAddress { get; set; }


		[Required]
		[BindRequired]
		[Display(Name = "پلاک")]
		[MaxLength(50, ErrorMessage = "حداکثر 10 حرف میتواند باشد")]
		public string Pelak { get; set; }


		[Required]
		[BindRequired]
		[Display(Name = "کدپستی")]
		[MaxLength(50, ErrorMessage = "یبلیبلیبلیبل")]
		//public string Country { get; set; }
		public int? PostalCode { get; set; }

	}
}

