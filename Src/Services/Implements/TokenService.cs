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

    /// <summary>
    /// Service implementation for managing JWT token creation.
    /// </summary>
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<User> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenService"/> class.
        /// </summary>
        /// <param name="config">The application configuration to retrieve JWT settings.</param>
        /// <param name="userManager">The UserManager for accessing user roles and details.</param>
        public TokenService(IConfiguration config,UserManager<User> userManager){
            _config = config;
            _userManager = userManager;
            var signingkey = _config["JWT:SigningKey"];
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingkey));
        }


        /// <summary>
        /// Creates a JWT token for the specified user.
        /// </summary>
        /// <param name="user">The user for whom the token is being created.</param>
        /// <returns>A string representation of the generated JWT token.</returns>
        public async Task<string> CreateToken(User user)
        {
            var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

           var claims = new List<Claim>{
            new Claim(JwtRegisteredClaimNames.NameId, user.Id!),
            new Claim(JwtRegisteredClaimNames.Name, user.name!),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName!),
            new Claim(ClaimTypes.Role, role)
           };

           var creds = new SigningCredentials(_key,SecurityAlgorithms.HmacSha256Signature);

           var tokenDescriptor = new SecurityTokenDescriptor{
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(1),
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