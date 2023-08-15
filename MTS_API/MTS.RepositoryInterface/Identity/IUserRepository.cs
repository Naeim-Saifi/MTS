using Microsoft.AspNetCore.Identity;
using MTS.DataAccess.Entities;
using MTS.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MTS.RepositoryInterface.Identity
{
    public interface IUserRepository
    {
        //List<UserModel> GetUserList(int roleId);
        /// <summary>
        /// Finds and returns a user, if any, who has the specified id.
        /// </summary>
        /// <param name="id">The user ID to search for.</param>
        /// <returns></returns>
        Task<UserModel> FindByIdAsync(string id);

        /// <summary>
        /// Find and returns a user, if any, who has the specified email.
        /// </summary>
        /// <param name="email">The user email to search for.</param>
        /// <returns></returns>
        Task<UserModel> FindByEmailAsync(string email);

        Task<UserModel> FindByNameAsync(string userName);

        /// <summary>
        /// Generate password reset token async.
        /// </summary>
        /// <param name="user">User object.</param>
        /// <returns></returns>
        Task<string> GeneratePasswordResetTokenAsync(string userId);

        /// <summary>
        /// Add the specified user to the named role.
        /// </summary>
        /// <param name="user">The user to add to the named role.</param>
        /// <param name="name">The name of the role to add the user to.</param>
        /// <returns></returns>
        Task AddToRoleAsync(User user, string role);

        /// <summary>
        /// Creates the specific user in the backing store.
        /// </summary>
        /// <param name="user">The user to create.</param>
        /// <returns></returns>
        Task<IdentityResult> CreateAsync(UserModel user, string role, string password);

        /// <summary>
        /// Generates an email confirmation token for the specified user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>
        /// An email confirmation token as a string.
        /// </returns>
        Task<string> GenerateEmailConfirmationTokenAsync(UserModel user);

        /// <summary>
        /// Validates that an email confirmation token matches the specified user.
        /// </summary>
        /// <param name="userID">The userID to validate the token against.</param>
        /// <param name="token">The email confirmation token to validate.</param>
        /// <returns></returns>
        Task<bool> ConfirmEmailAsync(string userID, string token);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IdentityResult> ResetPasswordAsync(string email, string code, string password);
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
