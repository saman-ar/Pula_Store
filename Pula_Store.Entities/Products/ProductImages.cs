using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Products
{
	public class ProductImages
	{
		public int Id { get; set; }

		[Required]
		public string Src { get; set; }

		public Product Product { get; set; }

		[ForeignKey(nameof(Product))]
		public int ProductId { get; set; }
	}
}
