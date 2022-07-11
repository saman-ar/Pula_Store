using Common.Extensions;
using Data.Repo;
using Entities.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebFramework.Extensions
{
	public static class PopulateCategorySelectList
	{
		public static void PopulateCategorySelectlist( this ICategorySelectList model , HttpContext httpContext)
		{
			var _categoryRepo = httpContext.RequestServices.GetService(typeof(CategoryRepo)) as CategoryRepo;

			var categories = _categoryRepo.GetAllParentCategory();
			var firstCategory = categories.First();

			model.ParentCategories = categories.ToSelectListItem();

			firstCategory = _categoryRepo.GetSubCategories(firstCategory);
			if (!firstCategory.SubCategories.IsNullOrEmpty())
				model.SubCategories = firstCategory.SubCategories.ToSelectListItem();
		}
	}
}
