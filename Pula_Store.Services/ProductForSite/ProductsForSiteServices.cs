using System;
using System.Collections.Generic;
using System.Text;
using Data.Repo;
using AutoMapper;
using Common.Extensions;
using Entities.Dtos.ProductDtos;
using Services.Extensions;

namespace Services.ProductForSite
{
    public class ProductsForSiteServices
    {
        private readonly ProductForSiteRepo _repo;
        public ProductsForSiteServices(ProductForSiteRepo repo)
        {
            _repo = repo;
        }
        public ProductsForSiteDtoPaging GetProducts(int page, int pageSize)
        {

            var products=_repo.GetProducts(page, pageSize);

            if (products.IsNullOrEmpty())
                return null;

            var mapProducts=Mapper.Map<List<ProductsForSiteDto>>(products);
            var productsDtoPaging = new ProductsForSiteDtoPaging()
            {
                Products = mapProducts,
                Page = page,
                PageSize = pageSize,
                ProductsCount = _repo.Count(),
            };

            return productsDtoPaging;
        }

        public ProductForSiteDto GetProductById(int id)
        {
            var product=_repo.GetProductById(id);
            if (product == null)
                return null;

            var mappedProduct=Mapper.Map<ProductForSiteDto>(product);
            //var mappedProduct = product.MapTo();

            return mappedProduct;
        }


    }
}
