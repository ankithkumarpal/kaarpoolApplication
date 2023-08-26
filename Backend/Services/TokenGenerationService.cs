using Interfaces;
using Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CarPool.Services
{
    public class TokenGenerationService : ITokenGeneration
    {
        private IConfiguration _config;
        private ILog _logger { get; set ;}
        public TokenGenerationService(IConfiguration config, ILog logger)
        {
            _config = config;
            _logger = logger;   
        }
        public string GenerateToken(User user)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var claims = new[]
                {
                new Claim("email",user.Email),
                new Claim("phoneno" , user.PhoneNo),
                new Claim("id" , user.Id.ToString())
                };
                var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                    _config["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddMinutes(20),
                    signingCredentials: credentials);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex) {
                _logger.Log(ex.Message);
                throw ex;
            }
        }
    }
}
