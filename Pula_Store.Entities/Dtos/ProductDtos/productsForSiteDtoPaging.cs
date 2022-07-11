using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.ProductDtos
{
    public class ProductsForSiteDtoPaging
    {
        public List<ProductsForSiteDto> Products { get; set; }
        public int ProductsCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
