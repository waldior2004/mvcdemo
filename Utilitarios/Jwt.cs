﻿using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace com.msc.infraestructure.utils
{
    public static class Jwt
    {
        public static string GenerateJWTAuthetication(string userName, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtHeaderParameterNames.Jku, userName),
                new Claim(JwtHeaderParameterNames.Kid, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, userName)
            };


            claims.Add(new Claim(ClaimTypes.Role, role));


            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(Convert.ToString(ConfigurationManager.AppSettings["JwtKey"])));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires =
                DateTime.Now.AddDays(
                    Convert.ToDouble(Convert.ToString(ConfigurationManager.AppSettings["JwtExpireDays"])));

            var token = new JwtSecurityToken(
                Convert.ToString(ConfigurationManager.AppSettings["JwtIssuer"]),
                Convert.ToString(ConfigurationManager.AppSettings["JwtAudience"]),
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);


        }


        public static string ValidateToken(string token)
        {
            if (token == null)
                return string.Empty;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Convert.ToString(ConfigurationManager.AppSettings["JwtKey"]));
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero

                }, out SecurityToken validatedToken);

                // Corrected access to the validatedToken
                var jwtToken = (JwtSecurityToken)validatedToken;
                var jku = jwtToken.Claims.First(claim => claim.Type == "jku").Value;
                var userName = jwtToken.Claims.First(claim => claim.Type == "kid").Value;

                return userName;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
