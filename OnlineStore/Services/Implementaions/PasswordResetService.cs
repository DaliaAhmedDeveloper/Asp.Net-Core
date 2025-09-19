namespace OnlineStore.Services;

using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using OnlineStore.Helpers;
using OnlineStore.Models;
using OnlineStore.Models.Dtos.Requests;
using OnlineStore.Models.Enums;
using OnlineStore.Repositories;

public class PasswordResetService : IPasswordResetService
{
    private readonly IPasswordResetRepository _passwordResetRepo;
    private readonly IUserRepository _userRepo;
    private readonly IEmailService _emailService;
    private readonly IStringLocalizer<PasswordResetService> _localizer;

    public PasswordResetService(
        IPasswordResetRepository passwordResetRepo,
        IUserRepository userRepo,
        IEmailService emailService,
        IStringLocalizer<PasswordResetService> localizer
        )
    {
        _passwordResetRepo = passwordResetRepo;
        _userRepo = userRepo;
        _emailService = emailService;
        _localizer = localizer;
    }

    // Request a password reset: generate token and send email
    public async Task<bool> RequestPasswordResetAsync(string email)
    {
        var user = await _userRepo.CheckUserAsync(email , UserType.User);
        if (user == null)
            throw new NotFoundException(_localizer["EmailNotFound"]); 

        var token = Guid.NewGuid().ToString("N"); // generate token
        var passwordReset = new PasswordReset
        {
            Email = email,
            Token = token,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddHours(1)
        };
        await _passwordResetRepo.AddAsync(passwordReset);
        // Send email
        await _emailService.SendEmailAsync(email, "Password Reset", $" Use This Token ( {token} ) Inside the App");
        return true;
    }

    // Reset the password using token
    public async Task<bool> ResetPasswordAsync(PasswordResetDto dto)
    {
         var user = await _userRepo.CheckUserAsync(dto.Email , UserType.User);
        if (user == null)
            throw new NotFoundException(_localizer["EmailNotFound"]); 

        var tokenEntry = await _passwordResetRepo.GetByEmailAndTokenAsync(dto.Email, dto.Token);
        if (tokenEntry == null) return false; // token invalid or expired

        // Update user password
        var hashedPassword = PasswordHelper.HashPassword(dto.NewPassword);
        user.PasswordHash = hashedPassword;
        await _userRepo.UpdateAsync(user);

        // Delete token
        await _passwordResetRepo.DeleteAsync(tokenEntry);
        return true;
    }
}
