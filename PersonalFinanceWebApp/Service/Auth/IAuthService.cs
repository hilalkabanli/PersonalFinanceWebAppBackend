using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalFinanceWebApp.Models.Dtos;
using PersonalFinanceWebApp.Models.Entities;

namespace PersonalFinanceWebApp.Service.Auth
{
    public interface IAuthService
    {
        public TokenResponse GenerateToken(User user);
        public string ExtractEmail(string token);
    }
}