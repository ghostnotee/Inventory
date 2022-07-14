using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Inventory.Domain.Entities;
using Inventory.Identity.Encryption;
using Inventory.Identity.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Inventory.Identity.Jwt;

public class JwtHelper : ITokenHelper
{
    public IConfiguration Configuration { get; }
    private TokenOptions _tokenOptions;
    private DateTime _accessTokenExpriration;
    private DateTime _refreshTokenExpiration;

    public JwtHelper(IConfiguration configuration)
    {
        Configuration = configuration;
        _tokenOptions = Configuration.GetSection(nameof(TokenOptions)).Get<TokenOptions>();
        _accessTokenExpriration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
        _refreshTokenExpiration = _accessTokenExpriration.AddMinutes(_tokenOptions.RefreshTokenExpiration);
    }

    public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
    {
        var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
        var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
        var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims);
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var token = jwtSecurityTokenHandler.WriteToken(jwt);

        return new AccessToken
        {
            Token = token,
            TokenExpiration = _accessTokenExpriration,
            RefreshToken = GenerateRefreshToken(),
            RefreshTokenExpiration = _refreshTokenExpiration
        };
    }

    public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
        SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
    {
        var jwt = new JwtSecurityToken(
            issuer: tokenOptions.Issuer,
            audience: tokenOptions.Audience,
            expires: _accessTokenExpriration,
            notBefore: DateTime.Now,
            claims: SetClaims(user, operationClaims),
            signingCredentials: signingCredentials
        );

        return jwt;
    }

    private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
    {
        var claims = new List<Claim>();
        claims.AddNameIdentifier(user.Id);
        claims.AddEmail(user.EmailAddress);
        claims.AddName($"{user.FirstName} {user.LastName}");
        claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());

        return claims;
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}