using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.AppIdentities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Site.Models.ViewModels;
using Data.AppContext;
using Microsoft.EntityFrameworkCore;
using WebFramework.Dtos;
using Site.Models;
using WebFramework.ActionFilters;

namespace Pula_Store.Site.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class UserController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly AppDbContext _context;
		public UserController(UserManager<AppUser> userManager, AppDbContext context)
		{
			_userManager = userManager;
			_context = context;
		}

		public IActionResult All()
		{
			//List<UserDto> userDtos= _context.Users.Select(u=>new UserDto() { Id =u.Id,UserName=u.UserName, 
			List<UserDto> userDtos = _context.Users.Select(u => new UserDto() { Id = u.Id, UserName = u.UserName, Email = u.Email, PhoneNumber = u.PhoneNumber ,IsActive=u.IsActive}).AsNoTracking().ToList();
			return View(userDtos);
		}

		//[HttpPost]
		[Ajax("Post")]
		public IActionResult Delete(string userId)
		{
			if (string.IsNullOrEmpty(userId) || string.IsNullOrWhiteSpace(userId))
				//return RedirectToAction(nameof(UserController.List));
				return Json(new JsonData() { IsSuccess = false, Message = "کاربری انتخاب نشده است" });

			var user = _userManager.FindByIdAsync(userId).Result;
			if (user is null)
			{
				//ModelState.AddModelError("", "User Not Found!!");
				//return RedirectToAction(nameof(UserController.List));
				return Json(new JsonData() { IsSuccess = false, Message = "کاربری وجود ندارد" });
			}

			user.IsRemoved = true;
			_context.Entry(user).State = EntityState.Modified;
			_context.SaveChanges();
			//return RedirectToAction(nameof(UserController.List));

			return Json(new JsonData() { IsSuccess = true, Message = "کاربر با موفقیت حذف شد" });
		}

		//[HttpGet]
		//public async Task<IActionResult> Edit(string Id)
		//{
		//	if (string.IsNullOrEmpty(Id))
		//		return RedirectToAction("ShowAllUser", "Account");

		//	///find user and return View with model for edit
		//	var user = await _userManager.FindByIdAsync(Id);
		//	if (user == null)
		//		return RedirectToAction("ShowAllUser");

		//	return View(user);
		//}

		[HttpPost]
		//[ValidateAntiForgeryToken]
		[Ajax("Post")]
		public IActionResult Edit(EditViewModel model)
		{
			///متد ویرایش اطلاعات یوزر به دلیل بررسی policy 
			///روی پسورد و ایمیل چالشهایی دارد که باید در نطر داشت 

			if (!ModelState.IsValid)
				return Json(new JsonData { IsSuccess = false, Message = "اطلاعات را بدرستی وارد کنید" });

			try
			{
				///TODO:we have to add some validation after applying edit
				var user = _context.Users.Find(model.UserId);
				if (user == null)
					return Json(new JsonData { IsSuccess = false, Message = "یوزر یافت نشد" });

				user.UserName = model.FullName;
				user.PhoneNumber = model.PhoneNumber;

				_context.Entry(user).State = EntityState.Modified;
				_context.SaveChanges();
				return Json(new JsonData { IsSuccess = true, Message = "با موفقیت ویرایش شد" });
			}
			catch (Exception)
			{
				return Json(new JsonData { IsSuccess = false, Message = "خطا در اپدیت اطلاعات" });
			}
		}

		[Ajax("Post")]
		public IActionResult ChangeUserStatus(string userId)
		{
			if (!ModelState.IsValid)
				return Json(new JsonData() { IsSuccess = false, Message = "داده ها به درستی ارسال نشده است" });

			try
			{
				var user = _context.Users.Find(userId);
				if (user == null)
					return Json(new JsonData() { IsSuccess = false, Message = "یوزری با این مشخصات مجود ندارد" });

				user.IsActive = !user.IsActive;
				_context.Entry(user).State = EntityState.Modified;
				_context.SaveChanges();
				return Json(new JsonData
				{
					IsSuccess = true,
					Message = user.IsActive ? "با موفقیت فعال شد" : "با موفقیت غیرفعال شد "
				});
			}
			catch (Exception)
			{
				return Json(new JsonData
				{
					IsSuccess = false,
					Message = "ویرایش موفقیت امیز نبود"
				});
			}
		}
	}
}
