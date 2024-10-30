using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CompanyManager.Application.Common.Interfaces.Application.Managers;
using CompanyManager.Application.Common.Interfaces.Application.Services;
using CompanyManager.Application.Common.Models;
using CompanyManager.Domain.Common;
using CompanyManager.Domain.Entities;
using CompanyManager.Domain.Errors;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CompanyManager.Application.Common.Managers
{
	public class JwtTokenManager(
		IConfiguration configuration,
		IJwtTokenGenerator jwtTokenGenerator,
		IRefreshTokenGenerator refreshTokenGenerator,
		UserManager<ApplicationUser> userManager) : IJwtTokenManager
	{
		public async Task<Result<JwtTokenResponse>> GenerateJwtAndRefreshToken(string userEmail)
		{
			var userResult = await ValidateUser(userEmail);
			if (userResult.IsFailure)
			{
				return Result.Failure<JwtTokenResponse>(DomainErrors.User.UserNotFound);
			}

			var user = userResult.Value;
			var roles = await userManager.GetRolesAsync(user);
			var claims = await userManager.GetClaimsAsync(user);

			var jwtToken = await jwtTokenGenerator.GenerateToken(user, roles.ToList(), claims.ToList());
			var refreshToken = refreshTokenGenerator.GenerateRefreshToken();
			var refreshTokenExpiration = refreshTokenGenerator.GetRefreshTokenExpiry();

			user.SetRefreshToken(refreshToken, refreshTokenExpiration);
			await userManager.UpdateAsync(user);

			return Result.Success(JwtTokenResponse.Success(jwtToken, refreshToken));
		}

		public async Task<Result<JwtTokenResponse>> RefreshToken(string jwtToken, string refreshToken)
		{
			var principal = GetTokenPrincipal(jwtToken);

			if (principal == null || !ValidatePrincipalEmail(principal, out var userEmail))
			{
				return Result.Failure<JwtTokenResponse>(DomainErrors.Jwt.InvalidToken);
			}

			var userResult = await ValidateUser(userEmail);
			if (userResult.IsFailure)
			{
				return Result.Failure<JwtTokenResponse>(DomainErrors.User.UserNotFound);
			}

			var user = userResult.Value;
			if (user.RefreshToken != refreshToken || user.RefreshTokenExpiration < DateTime.UtcNow)
			{
				return Result.Success(JwtTokenResponse.Failed());
			}

			var result = await GenerateJwtAndRefreshToken(userEmail);

			return result;
		}

		private async Task<Result<ApplicationUser>> ValidateUser(string userEmail)
		{
			var user = await userManager.FindByEmailAsync(userEmail);
			return user != null
				? Result.Success(user)
				: Result.Failure<ApplicationUser>(DomainErrors.User.UserNotFound);
		}

		private bool ValidatePrincipalEmail(ClaimsPrincipal principal, out string? userEmail)
		{
			userEmail = principal.FindFirst(ClaimTypes.Email)?.Value;
			return !string.IsNullOrWhiteSpace(userEmail);
		}

		private ClaimsPrincipal? GetTokenPrincipal(string token)
		{
			var securityKey =
				new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Jwt:Secret").Value));
			var validationParameters = new TokenValidationParameters
			{
				IssuerSigningKey = securityKey,
				ValidateLifetime = false,
				ValidateActor = false,
				ValidateIssuer = false,
				ValidateAudience = false,
			};
			return new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out _);
		}
	}
}