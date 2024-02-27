using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CetGraduateApp.Entities;
using Microsoft.IdentityModel.Tokens;
using CetGraduateApp.Models;

namespace CetGraduateApp.Helpers;

public class AuthManager
{
    private readonly JWTSettings _jwtSettings;

    public AuthManager(JWTSettings jwtSettings)
    {
        _jwtSettings = jwtSettings;
    }

    public string HashPassword(string password)
    {
        // Generate a salt for the password hash
        string salt = BCrypt.Net.BCrypt.GenerateSalt();

        // Hash the password using the generated salt
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

        return hashedPassword;
    }
    public bool VerifyPassword(string inputPassword, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(inputPassword, hashedPassword);
    }

    public string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(
            _jwtSettings.SecretKey);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()), // Adding userId claim
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}