using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Entities.Interfaces;

namespace Entities.Dtos.ProductDtos
{
	public class NewProductDto : ICategorySelectList
	{
		public NewProductDto()
		{
			Images = new List<IFormFile>();
			Features = new List<FeaturesViewModel>();
		}

		[Display(Name = "نام محصول"), Required]
		public string Name { get; set; }

		[Display(Name = "نام برند")]
		public string Brand { get; set; }

		[Display(Name = "توضیحات")]
		public string Description { get; set; }

		[Display(Name = "قیمت")]
		public int? Price { get; set; }

		[Display(Name = "موجودی در انبار")]
		public int? Inventory { get; set; }

		[Required]
		public long CategoryId { get; set; }

		[Display(Name = "نشان داده شود؟")]
		public bool Displayed { get; set; }

		[Display(Name = "تصاویر محصول")]
		public List<IFormFile> Images { get; set; }

		[Display(Name = "ویژگیهای محصول")]
		public List<FeaturesViewModel> Features { get; set; }

		[BindNever]
		public ICollection<SelectListItem> ParentCategories { get; set; }

		[BindNever]
		public ICollection<SelectListItem> SubCategories { get; set; }
	}

	public class FeaturesViewModel
	{
		[Display(Name = "عنوان ویژگی")]
		public string Name { get; set; }

		[Display(Name = "مقدار ویژگی")]
		public string Value { get; set; }
	}
}
