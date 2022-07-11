using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.AppContext;
using Data.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Site.Models.ViewModels;
using WebFramework.Extensions;
using AutoMapper;
using Entities.Products;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Services;
using Entities.Dtos.ProductDtos;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebFramework.ActionFilters;
using Site.Models;
using Services.ProductForAdmin;

namespace Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {

        private readonly AppDbContext _context;
        private readonly CategoryRepo _categoryRepo;
        private readonly IHostingEnvironment _env;
        private readonly ProductForAdminRepo _productRepo;
        private readonly ProductsForAdminServices _service;

        public ProductController(AppDbContext context,
                                         CategoryRepo categoryRepo,
                                         IHostingEnvironment env,
                                         ProductForAdminRepo productRepo,
                                         ProductsForAdminServices service)
        {
            _context = context;
            _categoryRepo = categoryRepo;
            _env = env;
            _productRepo = productRepo;
            _service = service;
        }

        public IActionResult Index(int page = 1, int pageSize = 10)
        {
            var result = _service.GetProductsForAdmin(page, pageSize);
            if (result == null)
                return null;

            return View(result);
        }

        [HttpGet]
        public IActionResult NewProduct()
        {
            var product = new NewProductDto();
            product.PopulateCategorySelectlist(HttpContext);

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NewProduct(NewProductDto model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "موارد مشخص شده را درست وارد نکرده اید");
                model.PopulateCategorySelectlist(HttpContext);
                return View(model);
            }

            var result = _service.Add(model);
            if (result == 0)
            {
                model.PopulateCategorySelectlist(HttpContext);
                TempData["Message"] = "خطا در ثبت کالا";
                TempData["Result"] = false;

                return View(model);
            }

            TempData["Message"] = "با موفقیت ثبت شد";
            TempData["Result"] = true;
            return RedirectToAction(nameof(Index));
        }

        //[HttpPost]
        [Ajax("Post")]
        public IActionResult Delete(int id)
        {
            var result = _service.Delete(id);
            if (result > 0)
                return Json(new JsonData() { IsSuccess = true, Message = "با موفقیت حذف شد" });

            return Json(new JsonData() { IsSuccess = false, Message = "در حذف این محصول اشکالی وجود دارد" });
        }

        public IActionResult Detail(int id)
        {
            var product = _service.GetProductByIdForAdmin(id);
            if (product == null)
                return null;

            return View(product);
        }

        public IActionResult Edit(int id)
        {
            //TODO: Add Editing Code
            return null;
        }

        private int ReportSucced(int d)
        {
            throw new NotImplementedException();
        }

        private void ReportFailure()
        {
            throw new NotImplementedException();
        }

        //public abstract class Beverage
        //{
        //    protected string description = "";
        //    public string GetDescription()
        //    {
        //        return description;
        //    }

        //    public abstract double Cost();
        //}

        //public abstract class Decorator : Beverage
        //{
        //    public string GetDescription()
        //    {
        //        return "description" + " , and some food";
        //    }

        //}
    }
}