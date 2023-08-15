using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MTS.CommonLibrary.Logger.Abstraction;
using MTS.DataAccess.DBContext;
using MTS.DataAccess.Entities;
using MTS.DataAccess.Repository;
using MTS.Models;
using MTS.RepositoryInterface.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTS.Repository.Identity
{
    public class UserRepository : Repository<User, MTSDBContext>, IUserRepository
    {
        #region Private Properties
        private readonly IRoleRepository _roleRepository;
        private readonly UserManager<User> _userManager;
        private readonly IMTSLogger _logger;

        #endregion Private Properties

        #region Constructor

        public UserRepository(MTSDBContext context, IMapper mapper, UserManager<User> userManager,
            IRoleRepository roleRepository, IMTSLogger logger) : base(context, mapper)
        {
            _userManager = userManager;
            _roleRepository = roleRepository;
            _logger = logger;
        }

        #endregion Constructor
        #region Public Methods

        //public List<UserModel> GetUserList(int roleId)
        //{
        //    List<UserModel> users = null;

        //    try
        //    {
        //        var userList = _context.Users.Include(a => a.School).ToList();
        //        var userRoles = _context.UserRoles.ToList();
        //        var roles = _context.Roles.ToList();

        //        users = userList.Select(x => new UserModel()
        //        {
        //            FirstName = x.FirstName,
        //            LastName = x.LastName,
        //            //MiddleName = x.MiddleName,
        //            UserName = x.UserName,
        //            Email = x.Email,
        //            UserId = x.Id,
        //            Role = roles.Where(r => userRoles.Any(ur => ur.RoleId.Equals(r.Id) && ur.UserId.Equals(x.Id))).FirstOrDefault().Name

        //        }).ToList();

        //        if (roleId > 0)
        //        {
        //            users = users.Where(user => userRoles.Any(ur => ur.UserId.Equals(user.UserId) && ur.RoleId.Equals(roleId))).ToList();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Error(ex.Message.ToString());
        //    }
        //    return users;
        //}
        public async Task<UserModel> FindByIdAsync(string id)
        {
            //_logger.Information("Enter into method : SMSBusinessManager.Services.UserService.FindByIdAsync");
            var result = await _userManager.FindByIdAsync(id).ConfigureAwait(false);
            var finalresult = _mapper.Map<UserModel>(result);
            finalresult.UserId = result.Id;
            //_logger.Information("Exit from method : SMSBusinessManager.Services.UserService.FindByIdAsync");
            return finalresult;
        }
        public async Task<UserModel> FindByEmailAsync(string email)
        {
            ////_logger.Information("Enter into method : SMSBusinessManager.Services.UserService.FindByEmailAsync");
            var result = await _userManager.FindByEmailAsync(email).ConfigureAwait(false);
            var finalresult = _mapper.Map<UserModel>(result);
            finalresult.UserId = result.Id;
            ////_logger.Information("Exit from method : SMSBusinessManager.Services.UserService.FindByEmailAsync");
            return finalresult;
        }
        public async Task<UserModel> FindByNameAsync(string userName)
        {
            UserRepository userRepository = this;
            User user = await userRepository._userManager.FindByNameAsync(userName).ConfigureAwait(false);
            UserModel userModel = userRepository._mapper.Map<UserModel>(user);
            userModel.UserId = user.Id;
            return userModel;
        }
        public async Task<string> GeneratePasswordResetTokenAsync(string userId)
        {
            string token = null;
            try
            {
                //var user = _mapper.Map<User>(userModel);
                var result = await _userManager.FindByIdAsync(userId).ConfigureAwait(false);
                token = await _userManager.GeneratePasswordResetTokenAsync(result).ConfigureAwait(false);
            }
            catch (Exception ex)
            {

            }
            return token;
        }
        public async Task AddToRoleAsync(User user, string role)
        {
            //_logger.Information("Enter into method : SMSBusinessManager.Services.UserService.AddToRoleAsync");
            bool exists = await _roleRepository.RoleExistsAsync(role).ConfigureAwait(false);
            if (!exists)
                await _roleRepository.CreateAsync(role).ConfigureAwait(false);
            await _userManager.AddToRoleAsync(user, role).ConfigureAwait(false);
            //_logger.Information("Exit from method : SMSBusinessManager.Services.UserService.AddToRoleAsync");
        }

        public async Task<IdentityResult> CreateAsync(UserModel userModel, string role, string password)
        {
            //_logger.Information("Enter into method : SMSBusinessManager.Services.UserService.CreateAsync");
            var user = _mapper.Map<User>(userModel);
            var result = await _userManager.CreateAsync(user, password).ConfigureAwait(false);
            if (result.Succeeded)
            {
                await AddToRoleAsync(user, role).ConfigureAwait(false);
                //_logger.Information("Exit from method : SMSBusinessManager.Services.UserService.CreateAsync");
                return result;
            }
            else
            {
                //_logger.Information("Exit from method : SMSBusinessManager.Services.UserService.CreateAsync");
                return IdentityResult.Failed(result.Errors.FirstOrDefault());
            }
        }
        public async Task<IdentityResult> ResetPasswordAsync(string userId, string code, string password)
        {
            //_logger.Information("Enter into method : SMSBusinessManager.Services.UserService.ResetPasswordAsync");
            var user = await _userManager.FindByIdAsync(userId).ConfigureAwait(false);
            if (user != null)
            {
                var result = await _userManager.ResetPasswordAsync(user, code, password).ConfigureAwait(false);
                //_logger.Information("Exit from method : SMSBusinessManager.Services.UserService.CreateAsync");
                return result;
            }
            else
            {
                //_logger.Information("Exit from method : SMSBusinessManager.Services.UserService.CreateAsync");
                return null;
            }
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(UserModel userModel)
        {
            //_logger.Information("Enter into method : SMSBusinessManager.Services.UserService.GenerateEmailConfirmationTokenAsync");
            var user = _mapper.Map<User>(userModel);
            var result = await _userManager.GenerateEmailConfirmationTokenAsync(user).ConfigureAwait(false);
            //_logger.Information("Exit from method : SMSBusinessManager.Services.UserService.GenerateEmailConfirmationTokenAsync");
            return result;
        }

        public async Task<bool> ConfirmEmailAsync(string userId, string code)
        {
            //_logger.Information("Enter into method : SMSBusinessManager.Services.UserService.ConfirmEmailAsync");

            var userresult = await FindByIdAsync(userId).ConfigureAwait(false);
            var user = _mapper.Map<User>(userresult);
            if (user != null && (DateTime.UtcNow - user.MailActivationTime).TotalHours <= 72)
            {
                var result = await _userManager.ConfirmEmailAsync(user, code).ConfigureAwait(false);
                if (result != null)
                {
                    //_logger.Information("Exit from method : SMSBusinessManager.Services.UserService.ConfirmEmailAsync");
                    return result.Succeeded;
                }
            }
            //_logger.Information("Exit from method : SMSBusinessManager.Services.UserService.ConfirmEmailAsync");
            return false;
        }
        /// <summary>
        /// Checking user name is unique or not.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<bool> IsValidUser(string userName)
        {
            try
            {
                User user = await _userManager.FindByNameAsync(userName).ConfigureAwait(false);
                return user == null;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
            return false;
        }

        public async Task<bool> IsValidEmail(string userEmail)
        {
            User user = await _userManager.FindByEmailAsync(userEmail).ConfigureAwait(false);
            return user == null;
        }

        #endregion
    }
}
