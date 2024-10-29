using System.Security.Cryptography;
using CompanyManager.Application.Common.Abstraction.Messaging;
using CompanyManager.Application.Common.Interfaces.Application.Services;
using CompanyManager.Domain.Common;
using CompanyManager.Domain.Entities;
using CompanyManager.Domain.Errors;
using Microsoft.AspNetCore.Identity;

namespace CompanyManager.Application.Actions.AuthActions.Commands.LoginUser;

public class LoginUserCommandHandler(
	UserManager<ApplicationUser> userManager,
	IJwtTokenGenerator jwtTokenGenerator,
	IRefreshTokenGenerator refreshTokenGenerator)
	: ICommandHandler<LoginUserCommand, LoginUserResponse>
{
	// Refactored Handle method
	public async Task<Result<LoginUserResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
	{
		var user = await userManager.FindByEmailAsync(request.Email);

		if (user == null)
		{
			return Result.Failure<LoginUserResponse>(DomainErrors.User.UserNotFound);
		}

		if (!await userManager.CheckPasswordAsync(user, request.Password))
		{
			return Result.Failure<LoginUserResponse>( DomainErrors.User.InvalidPassword);
		}

		var result = await GenerateTokens(user);

		return Result.Success(result);
	}

	private async Task<LoginUserResponse> GenerateTokens(ApplicationUser user)
	{
		var roles = await userManager.GetRolesAsync(user);
		var claims = await userManager.GetClaimsAsync(user);

		var jwtToken = await jwtTokenGenerator.GenerateToken(user, roles.ToList(), claims.ToList());
		var refreshToken = refreshTokenGenerator.GenerateRefreshToken();
		var refreshTokenExpiration = refreshTokenGenerator.GetRefreshTokenExpiry();

		user.SetRefreshToken(refreshToken, refreshTokenExpiration);
		await userManager.UpdateAsync(user);

		return new LoginUserResponse(jwtToken, refreshToken);
	}
}