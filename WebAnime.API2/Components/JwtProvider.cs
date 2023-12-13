using System;
using System.IdentityModel.Tokens.Jwt;
using DataModels.Helpers;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DataModels.EF.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace WebAnime.API2.Components
{
    public static class JwtProvider
    {
        public static string Issuer => "TalonEzio";
        public static string Audience => "All";
        public static SymmetricSecurityKey SecurityKey =>
            new SymmetricSecurityKey(Encoding.Unicode.GetBytes(AuthConstants.TokenProtectionKey));

        public static string GenerateJwt(Users user, IList<string> roleList)
        {
            if (user == null) return String.Empty;
            var credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.FullName),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role,String.Join(",",roleList))
            };


            var jwtToken = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                expires: DateTime.UtcNow.AddMinutes(3),
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
        public string UserName { get; set; }
    }
}