using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using PersonalFinanceWebApp.Helpers;
using PersonalFinanceWebApp.Models.Dtos;
using PersonalFinanceWebApp.Models.Entities;

namespace PersonalFinanceWebApp.Service.Auth
{
public class AuthService : IAuthService
    {
        public TokenResponse GenerateToken(User user)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AuthSettings.PrivateKey);
            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GenerateClaims(user),
                Expires = DateTime.UtcNow.AddMinutes(45),
                SigningCredentials = credentials,
            };

            var token = handler.CreateToken(tokenDescriptor);
            return new TokenResponse(handler.WriteToken(token));
        }

        private static ClaimsIdentity GenerateClaims(User user)
        {
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.Name, user.Email));

            return claims;
        }

        public string ExtractEmail(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AuthSettings.PrivateKey);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            var claimsPrincipal = handler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
            var email = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value;
            
            return email;
        }
    }
}