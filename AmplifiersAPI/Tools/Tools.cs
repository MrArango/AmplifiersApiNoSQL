using AmplifiersAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AmplifiersAPI.Tools
{
    public static class Tools
    {
        static public Response FillEx(Exception ex)
        {
            Response _res = new Response
            {
                Data = null,
                Status = false,
                Message = "Error en la peticion: " + ex.Message
            };

            return _res;
        }

        static public string GenerateToken(Users userT, string secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var Key = Encoding.ASCII.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, userT.Id.ToString()),
                        new Claim(ClaimTypes.Name, userT.Username)
                    }
                    ),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
