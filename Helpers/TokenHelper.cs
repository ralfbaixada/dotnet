using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;

namespace Helpers
{
    public static class TokenHelper
    {
        public static string GenerateJwtToken(string email, IConfiguration config, List<int> claimsUser)
        {
            var jwtSettings = config.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];
            var durationInMinutes = Convert.ToInt32(jwtSettings["DurationInMinutes"]);

            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            string customRoleClaimType = "Roles";

            var claims = new List<Claim> { };

            foreach (var claim in claimsUser)
            {
                claims.Add(new Claim(customRoleClaimType, claim.ToString()));
            }

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(durationInMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static string RenewJwtToken(string expiredToken, IConfiguration config)
        {
            var jwtSettings = config.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            var issuer = jwtSettings["Issuer"];
            var durationInMinutes = Convert.ToInt32(jwtSettings["DurationInMinutes"]);

            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = tokenHandler.ReadJwtToken(expiredToken);

            if (tokenDescriptor.ValidTo > DateTime.UtcNow && tokenDescriptor.ValidTo.Subtract(TimeSpan.FromMinutes(5)) < DateTime.UtcNow)
            {
                var newClaimsIdentity = new ClaimsIdentity(tokenDescriptor.Claims);
                var newTokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = newClaimsIdentity,
                    Expires = DateTime.UtcNow.AddMinutes(durationInMinutes),
                    SigningCredentials = credentials,
                    Issuer = issuer,
                };

                var renewedToken = tokenHandler.CreateToken(newTokenDescriptor);
                return tokenHandler.WriteToken(renewedToken);
            }
            else
            {
                return expiredToken;
            }
        }
    }
}
