using System;
using System.Collections.Generic;
using System.Text;
using Entities.Products;
using Entities.Dtos.ProductDtos;
using System.Linq;

namespace Services.Extensions
{
    public static class ProductExtensions
    {
        //public static ProductForSiteDto MapTo(this Product product)
        //{
        //    var mappedProduct = new ProductForSiteDto()
        //    {
        //        Id = product.Id,
        //        Brand = product.Brand,
        //        CategoryName = product.Category.Name + " - " + product.Category.ParentCategory?.Name,
        //        Name = product.Name,
        //        Description = product.Description,
        //        Features = product.Features.Select(f => new FeatureForSiteDto() { Name = f.Name, Value = f.Value }).ToList(),
        //        ImageSources = product.Images.Select(i => i.Src).ToList(),
        //        Price = product.Price.Value
        //    };

        //    return mappedProduct;
        //}
    }
}
