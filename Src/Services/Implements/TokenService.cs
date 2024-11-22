using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Taller1_WebMovil.Src.Models;
using Taller1_WebMovil.Src.Services.Interfaces;

namespace Taller1_WebMovil.Src.Services.Implements
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<User> _userManager;

        public TokenService(IConfiguration config,UserManager<User> userManager){
            _config = config;
            _userManager = userManager;
            var signingkey = _config["JWT:SigningKey"];
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingkey));
        }
        public async Task<string> CreateToken(User user)
        {
            var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

           var claims = new List<Claim>{
            new Claim(JwtRegisteredClaimNames.NameId, user.rut!),
            new Claim(JwtRegisteredClaimNames.Name, user.name!),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName!),
            new Claim(ClaimTypes.Role, role)
           };

           var creds = new SigningCredentials(_key,SecurityAlgorithms.HmacSha256Signature);

           var tokenDescriptor = new SecurityTokenDescriptor{
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = creds,
            Issuer = _config["JWT:Issuer"],
            Audience = _config["JWT:Audience"]
           };

           var tokenHandler = new JwtSecurityTokenHandler();
           var token = tokenHandler.CreateToken(tokenDescriptor);
           return tokenHandler.WriteToken(token);
        }
    }
}