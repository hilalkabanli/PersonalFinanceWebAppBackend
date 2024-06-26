using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalFinanceWebApp.Models.Dtos.auth
{
    public class TokenRequest
    {
        public required string Token { get; set; }
    }
}