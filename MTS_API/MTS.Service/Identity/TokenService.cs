using MTS.Models;
using MTS.Repository.Identity;
using MTS.RepositoryInterface.Identity;
using MTS.ServiceInterface.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MTS.Service.Identity
{
    public class TokenService :ITokenService
    {
        private readonly ITokenRepository _tokenRepository;

        public TokenService(ITokenRepository tokenRepository)
        {
           _tokenRepository = tokenRepository;
        }
        public async Task<Tokens> GenerateToken(string userName, string password)
        {
            var token = await _tokenRepository.ValidateAndGenerateToken(userName, password);
            return token;
        }
    }
}
