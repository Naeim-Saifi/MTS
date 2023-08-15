using AutoMapper;
using MTS.Contracts.Request;
using MTS.Contracts.Response;
using MTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTS.Mapping
{
    public class MTSContractMappingConfiguration:Profile
    {
        public MTSContractMappingConfiguration()
        {

            CreateMap<UserResponseModel, RegistrationRequestModel>();
            CreateMap<RegistrationRequestModel, UserResponseModel>();

            CreateMap<UserResponseModel, UserModel>();
            CreateMap<UserModel, UserResponseModel>();

            CreateMap<MedicineRequestModel, MedicineModel>();
            CreateMap<MedicineModel, MedicineResponseModel>();

            CreateMap<CategoryRequestModel, CategoryModel>();
            CreateMap<CategoryModel, CategoryResponse>();

            CreateMap<ProductRequestModel, ProductModel>();
            CreateMap<ProductModel, ProductResponseModel>();

        }
    }
}
