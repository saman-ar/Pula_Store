using Common.Extensions;
using Entities.Products;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace WebFramework.Extensions
{
	public static class CategoriesToSelectExtension
	{
		/// <summary>
		/// convert Category Item To SelectListItem
		/// </summary>
		/// <param name="List<Categories></categories>"></param>
		/// <returns>ICollection<SelectListItem></returns>
		public static ICollection<SelectListItem> ToSelectListItem(this ICollection<Category> categories /*,string name ,string value*/)
		{
			var selectListItems = new List<SelectListItem>();

			if (!categories.IsNullOrEmpty())
			{
				foreach (var item in categories)
					selectListItems.Add(new SelectListItem(item.Name, item.Id.ToString()));
			}

			return selectListItems;
		}

		//public static ICollection<SelectListItem> ToSelectListItem(this ICollection<Category> categories, string name, string value)
		//{
		//	var selectListItems = new List<SelectListItem>();
		//	foreach (var item in categories)
		//	{
		//		var propInfo=typeof(Category);

		//		item.GetType().GetProperty(name, System.Reflection.BindingFlags.Public).
		//		selectListItems.Add(new SelectListItem(item.Name, item.Id.ToString()));
		//	}
		//	return selectListItems;
		//}
	}
}
