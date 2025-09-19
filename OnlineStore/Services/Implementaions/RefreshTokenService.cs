namespace OnlineStore.Services;

using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using OnlineStore.Helpers;
using OnlineStore.Models;
using OnlineStore.Models.Dtos.Responses;
using OnlineStore.Repositories;
public class RefreshTokenService : IRefreshTokenService
{
    private readonly IRefreshTokenRepository _refreshTokenRepo;
    private readonly AppSettings _appSettings;
    private readonly IStringLocalizer<RefreshTokenService> _localizer;

    public RefreshTokenService(IRefreshTokenRepository refreshTokenRepo, IOptions<AppSettings> settings, IStringLocalizer<RefreshTokenService> localizer)
    {
        _refreshTokenRepo = refreshTokenRepo;
        _appSettings = settings.Value;
        _localizer = localizer;
    }

    // refresh the token
    public async Task<UserResponseDto?> Refresh(string refreshTokenString)
    {
        // Lookup refresh token in DB
        var refreshToken = await _refreshTokenRepo.GetWithUser(refreshTokenString);

        // check if its found in database
        if (refreshToken == null)
            throw new NotFoundException(_localizer["InvalidToken"]);

        //check if expired 
        DateTime now = DateTime.UtcNow;
        if (refreshToken.ExpiryDate <= DateTime.UtcNow)
            throw new UnauthorizedAccessException(_localizer["TokenExpired"]);

        var user = refreshToken.User;
        var Claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };
        //create new access token
        var accessToken = JwtHelper.GenerateAccessToken(Claims, _appSettings.JwtSecurityKey, _appSettings.Issuer, _appSettings.Audience);
        //create new refresh token
        var newRefreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        // update refreshToken in DB
        refreshToken.Token = newRefreshToken;
        refreshToken.ExpiryDate = DateTime.Now.AddDays(7);
        refreshToken.IsRevoked = false;

        await _refreshTokenRepo.UpdateAsync(refreshToken);
        
        //generate response
        var response = UserResponseDto.FromModel(user);
        response.Token = accessToken;
        response.RefreshToken = newRefreshToken;

        return response;
    }

    // get rfresh token by user id 
    public async Task<RefreshToken?> GetByUserId(int userId)
    {
        return await _refreshTokenRepo.GetByUserIdAsync(userId);
    }

    // delete refresh token
    public async Task<bool> Delete(RefreshToken refreshToken)
    {
        return await _refreshTokenRepo.DeleteAsync(refreshToken.Id);
    }
}