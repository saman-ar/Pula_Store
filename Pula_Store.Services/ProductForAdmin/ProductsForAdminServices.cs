using Data.Repo;
using System;
using System.Collections.Generic;
using AutoMapper;
using Entities.Dtos.ProductDtos;
using Entities.Products;
using Microsoft.AspNetCore.Http;
using Common.Extensions;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Services.ProductForAdmin
{
   public class ProductsForAdminServices
   {
      private readonly ProductForAdminRepo _repo;
      private readonly IHostingEnvironment _env;
      public ProductsForAdminServices(ProductForAdminRepo repo, IHostingEnvironment env)
      {
         _repo = repo;
         _env = env;
      }

      public ProductsForAdminDtoPaging GetProductsForAdmin(int page, int pageSize)
      {
         var products = _repo.GetProductsForAdmin(page, pageSize);
         if (products.IsNullOrEmpty())
            return null;

         var productsDto = Mapper.Map<List<ProductsForAdminDto>>(products);
         var productsDtoPaging = new ProductsForAdminDtoPaging()
         {
            Products = productsDto,
            Page = page,
            PageSize = pageSize,
            ProductsCount = _repo.Count(),
         };

         return productsDtoPaging;
      }

      public ProductForAdminDetailDto GetProductByIdForAdmin(int id)
      {
         var product = GetProductById(id);

         if (product == null)
            return null;

         return Mapper.Map<ProductForAdminDetailDto>(product);
      }

      public int Add(NewProductDto model)
      {
         int addedRowNumber;
         var newProduct = Mapper.Map<Product>(model);

         try
         {
            if (!model.Images.IsEmpty())
               newProduct.Images = SaveImageAndSrc(model.Images);

            addedRowNumber = _repo.Add(newProduct);
         }
         catch (Exception ex)
         {
            return 0;
         }

         return addedRowNumber;
      }

      public int Delete(int id)
      {
         var product = GetProductById(id);
         if (product == null)
            return 0;

         return _repo.Delete(product);
      }

      private Product GetProductById(int id)
      {
         var product = _repo.GetProductByIdForAdmin(id);
         return product;
      }

      private ICollection<ProductImages> SaveImageAndSrc(IList<IFormFile> images)
      {
         string imageSrc;
         var productImages = new List<ProductImages>();
         foreach (var item in images)
         {
            imageSrc = SaveFile(item);

            if (imageSrc != null)
               productImages.Add(new ProductImages
               {
                  //Product = product,
                  Src = imageSrc,
               });
         }

         return productImages;
      }

      private string SaveFile(IFormFile file)
      {
         if (file == null || file.Length == 0)
            return null;

         string folder = $@"images\ProductImages\";

         var imageRootFolder = Path.Combine(_env.WebRootPath, folder);

         if (!Directory.Exists(imageRootFolder))
            Directory.CreateDirectory(imageRootFolder);

         string fileNewName = DateTime.Now.Ticks.ToString() + file.FileName;

         var filePath = Path.Combine(imageRootFolder, fileNewName);

         using (var fileStream = new FileStream(filePath , FileMode.Create))
         {
            file.CopyTo(fileStream);
         }

         return folder + fileNewName;
      }


   }
}
