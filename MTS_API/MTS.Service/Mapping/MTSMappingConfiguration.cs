using AutoMapper;
using MTS.DataAccess.Entities;
using MTS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MTS.Service.Mapping
{
    public class MTSMappingConfiguration : Profile
    {
        public MTSMappingConfiguration()
        {
            CreateMap<UserModel, User>();
            CreateMap<User, UserModel>();

            //CreateMap<UserResponseModel, RegistrationRequestModel>();
            //CreateMap<RegistrationRequestModel, UserResponseModel>();

            //CreateMap<UserResponseModel, UserModel>();
            //CreateMap<UserModel, UserResponseModel>();

            CreateMap<MedicineModel, Medicine>();
            CreateMap<Medicine, MedicineModel>();

            CreateMap<CategoryModel, Category>();
            CreateMap<Category, CategoryModel>();

            CreateMap<ProductModel, Product>();
            CreateMap<Product, ProductModel>();
        }

    }
}
