using Domain.Entities;
using Domain.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services.TokenService;

public class TokenService
{
    private readonly UserManager<User> _userManager;
    private readonly TokenSettings _tokenSettings;
    public TokenService(UserManager<User> userManager, TokenSettings tokenSettings)
    {
        _userManager = userManager;
        _tokenSettings = tokenSettings;
    }

    public async Task<AccessTokenModel> CreateAccessTokenAsync(User user)
    {
        IList<string> rolelist = await _userManager.GetRolesAsync(user);
        IList<Claim> claimList = GenereateClaimList(user, rolelist);
        AccessToken accessToken = GenereateJwt(claimList);

        AccessTokenModel resultModel = new AccessTokenModel
        {
            AccessToken = accessToken,
            Roles = rolelist
        };

        return resultModel;
    }


    public RefreshToken GenerateNewRefreshToken(User user, string ipAddress)
    {
        RefreshToken refreshToken = new()
        {
            UserId = user.Id,
            CreatedIp = ipAddress,
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            Expiration = DateTime.UtcNow.AddMinutes(_tokenSettings.RefreshTokenExpiration),
            CreatedDate = DateTime.UtcNow,
            TTL = _tokenSettings.RefreshTokenTTL
        };

        return refreshToken;
    }


    private AccessToken GenereateJwt(IList<Claim> claimList)
    {
        DateTime _accessTokenExp = DateTime.Now.AddMinutes(_tokenSettings.AccessTokenExpiration);
        SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.SecurityKey));
        SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
            issuer: _tokenSettings.Issuer,
            audience: _tokenSettings.Audience,
            claims: claimList,
            expires: _accessTokenExp,
            signingCredentials: signingCredentials
        );
        JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
        string? token = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);

        AccessToken accessToken = new AccessToken(token, _accessTokenExp);
        return accessToken;
    }

    private IList<Claim> GenereateClaimList(User user, IList<string> rolelist)
    {
        List<Claim> claimList = new List<Claim>();
        string userName = $"{user.FirstName} {user.LastName}";
        claimList.Add(new Claim(ClaimTypes.Name, userName));
        claimList.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        if (user.Email != null) claimList.Add(new Claim(ClaimTypes.Email, user.Email));
        if (rolelist.Any())
        {
            rolelist.ToList().ForEach(role => {
                if (!string.IsNullOrWhiteSpace(role)) claimList.Add(new Claim(ClaimTypes.Role, role));
            });
        }

        return claimList;
    }
}
