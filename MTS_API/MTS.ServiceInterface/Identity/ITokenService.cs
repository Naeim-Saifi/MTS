using MTS.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MTS.ServiceInterface.Identity
{
    public interface ITokenService
    {
        Task<Tokens> GenerateToken(string userName, string password);
    }
}
