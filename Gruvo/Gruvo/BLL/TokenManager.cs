using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Gruvo.BLL
{
    public class TokenManager
    {
        private static readonly string Secret = "LldkfkgsnASda123As45mAgnkApqweiWi";

        public static string GenerateToken(long id)
        {
            byte[] key = Encoding.UTF8.GetBytes(Secret);

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);

            token.Payload["user_id"] = id;

            return handler.WriteToken(token);
        }
    }
}
