using Entities.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Site.Models.ViewModels
{
	public class AddCategoryViewModel
	{
		public int? Id { get; set; }

		[MaxLength(100, ErrorMessage = "خیلی بزرگ شد")]
		[Display(Name = "نام طبقه بندی")]
		[BindRequired]
		[Required]
		public string Name { get; set; }

		public int? ParentId { get; set; }

		public bool? IsRemoved { get; set; }
	}

	public class AddSubCategoryViewModel
	{
		public int? Id { get; set; }

		[MaxLength(100, ErrorMessage = "خیلی بزرگ شد")]
		[Display(Name = "نام طبقه بندی")]
		[BindRequired]
		[Required]
		public string Name { get; set; }

		[BindRequired]
		public int ParentId { get; set; }

		//[NotMapped]
		[Column(TypeName ="bit")]
		public bool? IsRemoved { get; set; }
	}

	public class CategoryEditViewModel
	{
		[BindRequired]
		//[DatabaseGenerated(DatabaseGeneratedOption.Identity)] ///for test
		public int Id { get; set; }

		[MaxLength(100, ErrorMessage = "خیلی بزرگ شد")]
		[Display(Name = "نام طبقه بندی")]
		public string Name { get; set; }

		public int? ParentId { get; set; }

		public bool IsRemoved { get; set; }
	}


	public class CategoryViewModel : ICategorySelectList
	{
		public CategoryViewModel()
		{
			ParentCategories = new List<SelectListItem>();
			SubCategories = new List<SelectListItem>();
		}

		public ICollection<SelectListItem> ParentCategories { get; set; }
		public ICollection<SelectListItem> SubCategories { get; set; }
	}

	public class SubCategoryViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}


}
