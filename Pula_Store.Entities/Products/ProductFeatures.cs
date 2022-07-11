using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Products
{
	public class ProductFeatures
	{
		public int Id { get; set; }

		[Required,MaxLength(150)]
		public string Name { get; set; }

		[Required,MaxLength(150)]
		public string Value { get; set; }

		public Product Product { get; set; }
		[ForeignKey(nameof(Product))]
		public int ProductId { get; set; }
	}
}
