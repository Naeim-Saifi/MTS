using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MTS.CommonLibrary.Logger.Abstraction;
using MTS.Contracts.Response;
using MTS.Models;
using MTS.RepositoryInterface.Identity;
using MTS.ServiceInterface.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MTS.Service.Identity
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IMTSLogger _logger;

        public UserService(IUserRepository userRepository, IMapper mapper, IMTSLogger logger)
        {

            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;

        }

        public async Task<UserResponseModel> FindByEmailAsync(string email)
        {
            UserResponseModel userViewModel = null;
            try
            {
                var result = await _userRepository.FindByEmailAsync(email);
                userViewModel = _mapper.Map<UserResponseModel>(result);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message.ToString());
            }
            return userViewModel;
        }
        public async Task<IdentityResult> CreateAsync(UserResponseModel userViewModel, string role, string password)
        {
            IdentityResult result = null;
            try
            {
                var userModel = _mapper.Map<UserModel>(userViewModel);
                result = await _userRepository.CreateAsync(userModel, role, password);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message.ToString());
            }
            return result;
        }

        public async Task<bool> IsValidUser(string userName)
        {
            bool result = false;
            try
            {
                result = await _userRepository.IsValidUser(userName);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message.ToString());
            }
            return result;
        }

        public async Task<bool> IsValidEmail(string userEmail)
        {
            bool result = false;
            try
            {
                result = await _userRepository.IsValidEmail(userEmail);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message.ToString());
            }
            return result;
        }
    }
}
