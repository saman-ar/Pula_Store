using Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.ProductDtos
{
    public class ProductForAdminDetailDto
    {
        public ProductForAdminDetailDto()
        {
            Images = new List<ProductImages>();
            Features = new List<ProductFeatures>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int? Price { get; set; }
        public int? Inventory { get; set; }
        //public bool Displayed { get; set; }
        //public bool IsRemoved { get; set; }
        public string CategoryName { get; set; }
        public ICollection<ProductImages> Images { get; set; }
        public ICollection<ProductFeatures> Features { get; set; }
    }
}
