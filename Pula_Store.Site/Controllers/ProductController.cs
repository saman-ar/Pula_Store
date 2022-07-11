using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.ProductForSite;

namespace Site.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductsForSiteServices _productServices;

        public ProductController(ProductsForSiteServices productServices)
        {
            _productServices = productServices;
        }

        public IActionResult Index(int page = 1, int pageSize = 10)
        {
            var result = _productServices.GetProducts(page, pageSize);

            return View(result);
        }

        public IActionResult Detail(int id)
        {
            if (!ModelState.IsValid)
                return View();

            var product = _productServices.GetProductById(id);
            if (product is null)
            {
                ModelState.AddModelError("", "این محصول وجود ندارد");
                return View();
            }

;            return View(product);
        }
    }

}