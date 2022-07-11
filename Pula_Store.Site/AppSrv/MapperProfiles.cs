using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Entities.Dtos.ProductDtos;
using Entities.Products;
using Site.Models.ViewModels;

namespace Pula_Store.Site.AppSrv
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            #region Additional Configuration
            //AllowNullCollections = true;
            //DisableConstructorMapping();	

            //SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
            //DestinationMemberNamingConvention = new PascalCaseNamingConvention();
            //DestinationMemberNamingConvention = new ExactMatchNamingConvention();

            //ShouldMapField = fi => false; //don't map field
            /////map only props that has public or internal Get Method
            //ShouldMapProperty = pi => pi.GetMethod != null && (pi.GetMethod.IsPublic || pi.GetMethod.IsPrivate);
            #endregion

            CreateMap<NewProductDto, Product>().ForMember(dest => dest.Images, opt => opt.Ignore());

            CreateMap<FeaturesViewModel, ProductFeatures>();

            CreateMap<Product, ProductsForAdminDto>()
                .ForMember(dest => dest.CategoryName,
                opts => opts.ResolveUsing(src => src.Category.ParentCategory.Name + " - " + src.Category.Name))
                ///TODO : do encryption on Id tp prevent iteration by client
                .ForMember(dest => dest.Id, opts => opts.ResolveUsing(src => src.Id));
            CreateMap<Product, ProductForAdminDetailDto>()
                .ForMember(dest => dest.CategoryName,
                opts => opts.ResolveUsing(src => src.Category.ParentCategory.Name + " - " + src.Category.Name));

            CreateMap<Product, ProductsForSiteDto>()
                .ForMember(dest => dest.ImageSrc, opts => opts.ResolveUsing(src => src.Images.FirstOrDefault().Src))
                .ForMember(dest => dest.Star, opts => opts.ResolveUsing(_ =>
                         {
                             Random random = new Random();
                             return random.Next(1, 5);
                         }));

            CreateMap<Product, ProductForSiteDto>()
                .ForMember(dest => dest.CategoryName,
                opts => opts.ResolveUsing(src => src.Category.ParentCategory.Name + " - " + src.Category.Name))
                .ForMember(dest => dest.Star, opts => opts.ResolveUsing(_ =>
                 {
                     Random random = new Random();
                     return random.Next(1, 5);
                 }))
                 .ForMember(dest => dest.ImageSources, opts => opts.MapFrom(src => src.Images));



        }
    }
}
