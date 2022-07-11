using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.ProductDtos
{
    public class ProductsForSiteDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string ImageSrc { get; set; }
        public int Star { get; set; }
    }
}
