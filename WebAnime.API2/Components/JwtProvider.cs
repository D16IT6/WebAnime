using System;
using System.IdentityModel.Tokens.Jwt;
using DataModels.Helpers;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DataModels.EF.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using JwtConstants = DataModels.Helpers.JwtConstants;

namespace WebAnime.API2.Components
{
    public static class JwtProvider
    {
        public static SymmetricSecurityKey SecurityKey =>
            new SymmetricSecurityKey(Encoding.Unicode.GetBytes(AuthConstants.TokenProtectionKey));

        public static string GenerateJwt(Users user, IList<string> roleList)
        {
            if (user == null) return String.Empty;
            var credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role,String.Join(",",roleList))
            };


            var jwtToken = new JwtSecurityToken(
                issuer: JwtConstants.Issuer,
                audience: JwtConstants.Audience,
                expires: DateTime.UtcNow.AddMinutes(JwtConstants.ExpriedAfterMinutes),
                signingCredentials: credentials,
                claims: claims
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return token;
        }
    }

    public class RefreshTokenRequest
    {
        public string RefreshToken { get; set; }
        public int UserId { get; set; }
    }
}