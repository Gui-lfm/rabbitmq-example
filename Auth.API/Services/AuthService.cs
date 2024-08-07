namespace Auth.API.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Auth.API.DTO;
using Microsoft.IdentityModel.Tokens;

public static class AuthService
{

  public static string GenerateToken(UserResponseDTO user)
  {
    var claims = new ClaimsIdentity();
    claims.AddClaim(new Claim(ClaimTypes.Email, user.Email!));

    var tokenHandler = new JwtSecurityTokenHandler();
    var tokenDescriptor = new SecurityTokenDescriptor()
    {
      Subject = claims,
      SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.ASCII.GetBytes("4d82a63bbdc67c1e4784ed6587f3730c")),
            SecurityAlgorithms.HmacSha256Signature
        ),
      Expires = DateTime.Now.AddHours(6)
    };
    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
  }
}