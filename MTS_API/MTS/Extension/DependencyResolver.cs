using MTS.CommonLibrary.Logger.Abstraction;
using MTS.CommonLibrary.Logger.Implementation;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using MTS.DataAccess.Repository;
using MTS.Mapping;
using MTS.Service.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MTS.RepositoryInterface;
using MTS.Repository;
using MTS.ServiceInterface;
using MTS.Service;
using MTS.CommonLibrary.Email.Abstraction;
using MTS.CommonLibrary.Email.Implementation;
using MTS.ServiceInterface.Identity;
using MTS.Service.Identity;
using MTS.RepositoryInterface.Identity;
using MTS.Repository.Identity;

namespace MTS.Extension
{
    public static class DependencyResolver
    {
        public static IServiceCollection ResigerDependency(this IServiceCollection services)
        {

            #region Mapper Config

            services.AddAutoMapper(typeof(MTSMappingConfiguration), typeof(MTSContractMappingConfiguration));

            #endregion


            #region Log

            services.AddSingleton<IMTSLogger, MTSLogger>();

            #endregion


            #region Repository Helpers

            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<IMedicineRepository, MedicineRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            #endregion


            #region Service Layer

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            //services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IMedicineService, MedicineService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();

            #endregion

            services.AddTransient<IEmailSender, EmailSender>();

            return services;
        }
    }
}
