using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.AppContext;
using Data.Repo;
using Entities.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Site.Models;
using Site.Models.ViewModels;
using WebFramework.ActionFilters;
using WebFramework.Extensions;

namespace Site.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class CategoryController : Controller
	{
		private readonly AppDbContext _context;
		private readonly CategoryRepo _categoryRepo;

		public CategoryController(AppDbContext context, CategoryRepo categoryRepo)
		{
			_context = context;
			_categoryRepo = categoryRepo;
		}

		public IActionResult All()
		{
			//var categories = _categoryRepo.GetAllParentCategory();

			//if (categories.IsNullOrEmpty())
			//	return View(new CategoryViewModel());

			var categoryViewModel = new CategoryViewModel();
			categoryViewModel.PopulateCategorySelectlist(HttpContext);

			//categoryViewModel.ParentCategories = categories.ToSelectListItem();

			//var firstCategory = categories.First();
			//firstCategory = _categoryRepo.GetSubCategories(firstCategory);
			
			//if (!firstCategory.SubCategories.IsNullOrEmpty())
			//	categoryViewModel.SubCategories = firstCategory.SubCategories.ToSelectListItem();

			return View(categoryViewModel);
		}

		[HttpPost]
		[Ajax("Post")]
		public IActionResult AddCategory(AddCategoryViewModel model)
		{
			if (!ModelState.IsValid)
				return Json(new JsonData() { IsSuccess = false, Message = "اطلاعات کامل وارد نشده است" });

			if (model.ParentId != null)
			{
				var result = _context.Categories.SingleOrDefault(c => c.Id == model.ParentId);
				if (result is null)
					return Json(new JsonData() { IsSuccess = false, Message = "پدر این دسته بندی یافت نشد" });
			}

			var category = new Category()
			{
				Name = model.Name,
				ParentId = model.ParentId,
				IsRemoved = false
			};

			_context.Categories.Add(category);
			_context.SaveChanges();

			return Json(new JsonData() { IsSuccess = true, Message = "با موفقیت ثبت شد" });
		}

		[Ajax("Post")]
		public IActionResult RemoveCategory(int Id, int parentId)
		{
			if (parentId is 0) // is category or subcategory
			{
                ///is subcate
                //TODO:delete cate and its subcates
                var category = _context.Categories.Include(c => c.SubCategories).SingleOrDefault(c => c.Id == Id);
				if (category is null)
					return Json(new JsonData() { IsSuccess = false, Message = "یافت نشد" });

				if (category.SubCategories.Count != 0)
				{
					foreach (var subcategory in category.SubCategories)
					{
						subcategory.IsRemoved = true;
						_context.Entry(subcategory).State = EntityState.Modified;
					}
				}
				category.IsRemoved = true;
				_context.Entry(category).State = EntityState.Modified;
			}
			else
			{
				///is subcate
				//TODO:delete subcate
				var category = _context.Categories.Find(Id);
				if (category == null)
					return Json(new JsonData() { IsSuccess = false, Message = "یافت نشد" });

				category.IsRemoved = true;
				_context.Entry(category).State = EntityState.Modified;
			}

			_context.SaveChanges();
			return Json(new JsonData() { IsSuccess = true, Message = "با  موفقیت حذف شد" });
		}

		[Ajax("Post")]
		public IActionResult EditCategory(CategoryEditViewModel model)
		{
			if (!ModelState.IsValid)
				return Json(new JsonData() { IsSuccess = false, Message = "اطلاعات کامل وارد نشده است" });

			var result = _context.Categories.SingleOrDefault(c => c.Id == model.Id);
			if (result == null)
				return Json(new JsonData() { IsSuccess = false, Message = "این دسته بندی وجود ندارد" });

			result.Name = model.Name;
			//ParentId = model.ParentId,
			//IsRemoved = false

			_context.Entry(result).State = EntityState.Modified;
			_context.SaveChanges();

			return Json(new JsonData() { IsSuccess = true, Message = "با موفقیت ویرایش شد" });
		}

		[Ajax("Post")]
		public IActionResult AddSubCategory(AddSubCategoryViewModel model)
		{
			if (!ModelState.IsValid)
				return Json(new JsonData() { IsSuccess = false, Message = "اطلاعات کامل وارد نشده است" });

			var result = _context.Categories.SingleOrDefault(c => c.Id == model.ParentId);
			if (result == null)
				return Json(new JsonData() { IsSuccess = false, Message = "پدر این دسته بندی یافت نشد" });

			var subCategory = new Category()
			{
				Name = model.Name,
				ParentId = model.ParentId,
				IsRemoved = false
			};

			_context.Categories.Add(subCategory);
			_context.SaveChanges();

			return Json(new JsonData() { IsSuccess = true, Message = "زیر شاخه با موفقیت ثبت شد" });
		}

		[Ajax("Post")]
		public ActionResult ShowSubCategory(int id)
		{
			if (!ModelState.IsValid)
				return Json(new JsonData() { IsSuccess = false, Message = "اطلاعات کامل وارد نشده است" });

			var result = _context.Categories.Include(c => c.SubCategories).SingleOrDefault(c => c.Id == id);//.Select(c=>new SubCategoryViewModel());

			if (result == null)
				return Json(new JsonData() { IsSuccess = false, Message = "پدر این دسته بندی یافت نشد" });

			if (!result.SubCategories.Any() || result.SubCategories.Count == 0)
				return Json(new JsonData() { IsSuccess = false, Message = "زیر مجموعه ای برای این مورد وجود ندارد" });


			var data = new JsonData()
			{
				IsSuccess = true,
				Data = result.SubCategories.Select(c => new SubCategoryViewModel() { Id = c.Id, Name = c.Name }).ToList()
			};

			return Json(data);
		}
	}
}