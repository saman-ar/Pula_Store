using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Products
{
	public class Product
	{
		public Product()
		{
			Images = new List<ProductImages>();
			Features = new List<ProductFeatures>();
		}
		public int Id { get; set; }

		[Required,MaxLength(150)]
		public string Name { get; set; }

		[MaxLength(100)]
		public string Brand { get; set; }

		public string Description { get; set; }

		public int? Price { get; set; }
		public int? Inventory { get; set; }
		public bool Displayed { get; set; }
		public bool IsRemoved { get; set; }

		public  Category Category { get; set; }
		[ForeignKey(nameof(Category))]
		public int? CategoryId { get; set; }

		public ICollection<ProductImages> Images { get; set; }
		public ICollection<ProductFeatures> Features { get; set; }
	}
}
