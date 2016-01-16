using AutoMapper;
using ECommerce.Dapper;
using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.AutoMapper
{
    public static class AutoMapperWebConfiguration
    {
        private static IClothesService _clothesService = new ClothesService();
        public static void Configure()
        {
            ConfigureMapping();
        }

        private static void ConfigureMapping()
        {
            Mapper.CreateMap<Clothes, ClothesViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color))
                .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => _clothesService.GetACategory(src.CategoryId).Name))
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => _clothesService.GetABrand(src.BrandId).Name));

            Mapper.CreateMap<ClothesViewModel, Clothes>();

            Mapper.CreateMap<ClothesCreateViewModel, Clothes>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color))
                .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.FilterCategory))
                .ForMember(dest => dest.BrandId, opt => opt.MapFrom(src => src.FilterBrand));

            Mapper.CreateMap<CategoryViewModel, Category>();
            Mapper.CreateMap<Category, CategoryViewModel>();
        } 
    }
}