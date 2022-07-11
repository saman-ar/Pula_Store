using Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Site.Models.ViewModels
{
	public class ProductsForAdminViewModel
	{
		public List<Product> Products { get; set; }
		public int WholeProducts { get; set; }
		public int Page { get; set; }
		public int PageSize { get; set; }

	}
}
