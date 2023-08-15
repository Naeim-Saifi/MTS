using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MTS.CommonLibrary.Logger.Abstraction;
using MTS.DataAccess.DBContext;
using MTS.DataAccess.Entities;
using MTS.DataAccess.Repository;
using MTS.RepositoryInterface.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MTS.Repository.Identity
{
    public class RoleRepository : Repository<Role, MTSDBContext>, IRoleRepository
    {
        private readonly IMTSLogger _logger;
        private readonly RoleManager<Role> _roleManager;
        public RoleRepository(MTSDBContext context, IMapper mapper, IMTSLogger logger, RoleManager<Role> roleManager) : base(context, mapper)
        {
            _logger = logger;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Creates the specified role in the persistence store.
        /// </summary>
        /// <param name="name">The name of the new role.</param>
        /// <returns></returns>
        public async Task CreateAsync(string name)
        {
            _logger.Information("Enter into method : SMSBusinessManager.Services.RoleService.CreateAsync");
            var role = new Role
            {
                Name = name
            };

            await _roleManager.CreateAsync(role).ConfigureAwait(false);
            _logger.Information("Exist from method : SMSBusinessManager.Services.RoleService.CreateAsync");
            /// <summary>
            /// Gets a flag indicating whether the specified roleName exists.
            /// </summary>
            /// <param name="name">The role name whose existence should be checked.</param>
            /// <returns>True if the role name exists, otherwise false.</returns>

        }
        public async Task<bool> RoleExistsAsync(string name)
        {
            _logger.Information("Enter into method : SMSBusinessManager.Services.RoleService.RoleExistsAsync");
            bool exists = await _roleManager.RoleExistsAsync(name).ConfigureAwait(false);

            _logger.Information("Exist from method : SMSBusinessManager.Services.RoleService.RoleExistsAsync");
            return exists;
        }
    }
}
