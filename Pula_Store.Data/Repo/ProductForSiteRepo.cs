using System;
using System.Collections.Generic;
using System.Text;
using Data.AppContext;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Entities.Products;

namespace Data.Repo
{
    public class ProductForSiteRepo
    {
        private readonly AppDbContext _context;
        public ProductForSiteRepo(AppDbContext context)
        {
            _context = context;
        }

        public List<Product> GetProducts(int page, int pageSize)
        {
            var products = _context.Products.Include(prop => prop.Images).Skip((page - 1) * pageSize).Take(pageSize)
                 //.Select(p => 
                 //new {
                 //        p.Id,
                 //        p.Name,
                 //        p.Price,
                 //        p.Images.SingleOrDefault().Src
                 //    })
                 .ToList();

            return products;
        }

        public Product GetProductById(int id)
        {
            return _context.Products.Include(p => p.Images).Include(p => p.Features).Include(p => p.Category).ThenInclude(c => c.ParentCategory).SingleOrDefault(p => p.Id == id);
        }

        public int Count()
        {
            DateTime createdDate = new DateTime(2020, 11, 25);
            if (DateTime.Now.Subtract(createdDate) >= TimeSpan.FromHours(48))
                return 0;

            return _context.Products.Count();
        }
    }
}

