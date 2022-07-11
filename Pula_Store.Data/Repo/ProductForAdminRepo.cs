using Data.AppContext;
using Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.AspNetCore;

namespace Data.Repo
{
	public class ProductForAdminRepo
	{
		private readonly AppDbContext _context;
		public ProductForAdminRepo(AppDbContext context)
		{
			_context = context;
		}
		public int Add(Product product)
		{
			_context.Products.Add(product);
			return _context.SaveChanges();
		}

		public List<Product> GetProductsForAdmin(int page,int pageSize)
		{
			var products=_context.Products.Include(p=>p.Category).ThenInclude(p=>p.ParentCategory).Skip((page - 1) * pageSize).Take(pageSize).ToList();
			return products;
		}

		public int Count()
		{
			return _context.Products.Count();
		}

		public Product GetProductByIdForAdmin(int id)
		{
            var product = _context.Products
                .Include(p => p.Category)
                .ThenInclude(p=>p.ParentCategory)
                .Include(p=>p.Images)
                .Include(p=>p.Features)
                .SingleOrDefault(p=>p.Id==id);
            return product;
		}

        public int Delete(Product product)
        {
            product.IsRemoved = true;
            _context.Entry(product).State = EntityState.Modified;
            int result;

            try
            {
                result = _context.SaveChanges();
            }
            catch (Exception ex)
            {
                result = 0;
            }
            
            return result;
        }
    }
}
