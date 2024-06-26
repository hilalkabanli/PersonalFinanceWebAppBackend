using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalFinanceWebApp.Models.Dtos
{
    public class TokenResponse
    {
        public string AccessToken { get; set; }

        public TokenResponse(string accessToken)
        {
            AccessToken = accessToken;
        }
    }
}