using Microsoft.AspNetCore.Identity;
using MTS.Contracts.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MTS.ServiceInterface.Identity
{
    public interface IUserService
    {
        //List<UserResponseModel> GetUserList(int roleId);
        ///// <summary>
        ///// Finds and returns a user, if any, who has the specified id.
        ///// </summary>
        ///// <param name="id">The user ID to search for.</param>
        ///// <returns></returns>
        //Task<UserResponseModel> FindByIdAsync(string id);

        ///// <summary>
        ///// Find and returns a user, if any, who has the specified email.
        ///// </summary>
        ///// <param name="email">The user email to search for.</param>
        ///// <returns></returns>
        Task<UserResponseModel> FindByEmailAsync(string email);

        //Task<UserResponseModel> FindByNameAsync(string userName);

        ///// <summary>
        ///// Generate password reset token async.
        ///// </summary>
        ///// <param name="user">User object.</param>
        ///// <returns></returns>
        //Task<string> GeneratePasswordResetTokenAsync(string userId);


        /// <summary>
        /// Creates the specific user in the backing store.
        /// </summary>
        /// <param name="user">The user to create.</param>
        /// <returns></returns>
        Task<IdentityResult> CreateAsync(UserResponseModel user, string role, string password);

        /// <summary>
        /// Generates an email confirmation token for the specified user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>
        /// An email confirmation token as a string.
        /// </returns>
        //Task<string> GenerateEmailConfirmationTokenAsync(UserResponseModel user);

        /// <summary>
        /// Validates that an email confirmation token matches the specified user.
        /// </summary>
        /// <param name="userID">The userID to validate the token against.</param>
        /// <param name="token">The email confirmation token to validate.</param>
        /// <returns></returns>
        //Task<bool> ConfirmEmailAsync(string userID, string token);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        //Task<IdentityResult> ResetPasswordAsync(string email, string code, string password);
        /// <summary>
        /// Checking user name is unique or not.
        /// </summary>
        /// <param name="userName"></param>
        ///<param name="userEmail"></param>
        /// <returns></returns>
        Task<bool> IsValidUser(string userName);

        /// <summary>
        /// Checking user  email is unique or not.
        /// </summary>
        /// <param name="userName"></param>
        ///<param name="userEmail"></param>
        /// <returns></returns>
        Task<bool> IsValidEmail(string userEmail);
    }
}
