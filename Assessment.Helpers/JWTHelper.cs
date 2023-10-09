using Assessment.DTO.DataObjects;
using Assessment.DTO.Response;
using Assessment.Helpers.Interface;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Helpers
{
    // this class has a method that generates a jwt and adds data that we could use in the payload
    public class JWTHelper : IJWTHelper
    {
        private readonly IOptions<JwtSettingsConfig> _jwtSettingsConfig;

        public JWTHelper(IOptions<JwtSettingsConfig> jwtSettingsConfig)
        {
            _jwtSettingsConfig = jwtSettingsConfig;
        }

        public async Task<string> GenerateJwt(AuthenticationResponse authentication)
        {
            var claims = new List<Claim>(new[]
            {
                new Claim(type: "ID", authentication.ID.ToString()),
                new Claim(type: "Username", authentication.Username),
                new Claim(type: "Name", authentication.Name),
                new Claim(type: "Surname", authentication.Surname),
                new Claim(type: "RoleID", authentication.RoleID.ToString()),
                new Claim(type: "Mobile", authentication.Mobile),
                new Claim(type: "Email", authentication.Email),
            });

            var token = new JwtSecurityToken
            (
                issuer: _jwtSettingsConfig.Value.Issuer,
                audience: _jwtSettingsConfig.Value.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettingsConfig.Value.ExpirationInMinutes),
                //notBefore: DateTime.Now,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettingsConfig.Value.Key)),
                    SecurityAlgorithms.HmacSha256)
            );

            return await Task.Run(() => new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
