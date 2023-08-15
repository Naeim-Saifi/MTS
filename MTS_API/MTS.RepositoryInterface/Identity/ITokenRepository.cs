using MTS.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MTS.RepositoryInterface.Identity
{
    public interface ITokenRepository
    {
        public Task<Tokens> ValidateAndGenerateToken(string userName, string password);
    }
}
