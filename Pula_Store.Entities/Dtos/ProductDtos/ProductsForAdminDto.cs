using Entities.Products;
using System;
using System.Text;

namespace Entities.Dtos.ProductDtos
{
	public class ProductsForAdminDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Brand { get; set; }
		public int? Price { get; set; }
		public int? Inventory { get; set; }
		public string CategoryName { get; set; }
	}
}
