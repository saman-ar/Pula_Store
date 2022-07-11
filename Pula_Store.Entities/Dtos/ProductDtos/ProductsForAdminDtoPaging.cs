using System.Collections.Generic;

namespace Entities.Dtos.ProductDtos
{
	public class ProductsForAdminDtoPaging
	{
		public List<ProductsForAdminDto> Products { get; set; }
		public int ProductsCount { get; set; }
		public int Page { get; set; }
		public int PageSize { get; set; }
	}
}
