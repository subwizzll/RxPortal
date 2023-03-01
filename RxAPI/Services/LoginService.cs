using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.OpenApi.Extensions;
using RxAPI.Config;
using RxAPI.Interfaces.Services;
using RxAPI.Models;

namespace RxAPI.Services;

public class LoginService : ILoginService
{
    readonly JwtConfig _jwtConfig;

    public LoginService(JwtConfig jwtConfig)
    {
        _jwtConfig = jwtConfig;
    }

    public string Login(UserModel userModel)
    {
        var accessToken = string.Empty;
        if (userModel is not { Trusted: true }) 
            return accessToken;

        var claims = new[]
        {
            new Claim(ClaimTypes.Email, userModel.Email),
            new Claim(ClaimTypes.Role, userModel.Role.GetDisplayName()),
        };

        var jwtSecurityToken = new JwtSecurityToken
        (
            issuer: _jwtConfig.Issuer,
            audience: _jwtConfig.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(5),
            notBefore: DateTime.UtcNow,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Key)),
                SecurityAlgorithms.HmacSha256)
        );
        accessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        return accessToken;
    }
}