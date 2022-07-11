using Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.ProductDtos
{
    public class ProductForSiteDto
    {
        public ProductForSiteDto()
        {
            ImageSources = new List<ImageSource>();
            Features = new List<FeatureForSiteDto>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Brand { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public int Star { get; set; }

        //public List<ProductImages> ImageSources { get; set; }
        public ICollection<ImageSource> ImageSources { get; set; }
        public List<FeatureForSiteDto> Features { get; set; }

    }

    



}
