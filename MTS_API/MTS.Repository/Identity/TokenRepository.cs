using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MTS.CommonLibrary.Logger.Abstraction;
using MTS.DataAccess.Entities;
using MTS.Models;
using MTS.RepositoryInterface.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MTS.Repository.Identity
{
    public class TokenRepository : ITokenRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserRepository _userRepository;
        private readonly IMTSLogger _logger;
        private readonly IConfiguration _iconfiguration;

        public TokenRepository(UserManager<User> userManager, SignInManager<User> signInManager, IUserRepository userRepository, IMTSLogger logger, IConfiguration iconfiguration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userRepository = userRepository;
            _logger = logger;
            _iconfiguration = iconfiguration;
        }
 

        public async Task<Tokens> ValidateAndGenerateToken(string userName, string password)
        {
            try
            {
                Tokens tokens = null;
                User user = await _userManager.FindByNameAsync(userName);
                if (user == null)
                {
                    return null;
                }

                SignInResult signInResult = await _signInManager.PasswordSignInAsync(userName, password, false, false);
                if (!signInResult.Succeeded)
                {
                    return null;
                }

                var role = await _userManager.GetRolesAsync(user).ConfigureAwait(false);//2
                var token = GenerateToken(userName, role);
                var userDetail = await _userRepository.FindByNameAsync(userName);
                userDetail.Role = String.Join(",", role);
                tokens = new Tokens() { User = userDetail, Token = token, Role=userDetail.Role };
                return tokens;

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return null;
            }
        }

        private string GenerateToken(string userName, IList<string> role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_iconfiguration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
              {
             new Claim(ClaimTypes.Name, userName),
              new Claim(ClaimTypes.Role, string.Join(",",role))
              }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
