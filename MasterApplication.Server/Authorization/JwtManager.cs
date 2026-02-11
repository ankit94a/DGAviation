

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using MasterApplication.DB.Services;

namespace MasterApplication.Server.Authorization
{
    public class JwtManager : IJwtManager
    {
        private readonly JwtConfig _jwtSettings;



        public JwtManager(IOptions<JwtConfig> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }
        public string GenerateJwtToken(UserClaims user)
        {

            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(MasterConstant.SessionId,user.SessionId),
                new Claim(MasterConstant.UserId, user.UserId.ToString()),
                new Claim(MasterConstant.RoleType,user.RoleType), 
                new Claim(MasterConstant.Name,user.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            return jwtTokenHandler.WriteToken(jwtTokenHandler.CreateToken(tokenDescriptor));
        }


    }
}


















