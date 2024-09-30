using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Application
{
    public class JwtService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;

        public JwtService(IConfiguration config)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        }

        public string CreateToken(User user)
        {
            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString())
        };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(15),  // Access Token expiration time
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        
        public RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                ExpiryDate = DateTime.Now.AddDays(7)  // Refresh token expiration time
            };
        }       
        
        public bool ValidateRefreshToken(string refreshToken)
        {
            // Logic to validate the refresh token and check expiry
            return true;
        }        
    }
    public class RefreshToken
    {
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}