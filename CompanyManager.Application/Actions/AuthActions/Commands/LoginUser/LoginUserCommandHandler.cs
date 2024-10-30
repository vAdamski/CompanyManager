using System.Security.Cryptography;
using CompanyManager.Application.Common.Abstraction.Messaging;
using CompanyManager.Application.Common.Interfaces.Application.Managers;
using CompanyManager.Application.Common.Interfaces.Application.Services;
using CompanyManager.Application.Common.Models;
using CompanyManager.Domain.Common;
using CompanyManager.Domain.Entities;
using CompanyManager.Domain.Errors;
using Microsoft.AspNetCore.Identity;

namespace CompanyManager.Application.Actions.AuthActions.Commands.LoginUser;

public class LoginUserCommandHandler(
	UserManager<ApplicationUser> userManager,
	IJwtTokenManager jwtTokenManager)
	: ICommandHandler<LoginUserCommand, JwtTokenResponse>
{
	public async Task<Result<JwtTokenResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
	{
		var user = await userManager.FindByEmailAsync(request.Email);

		if (user == null)
		{
			return Result.Failure<JwtTokenResponse>(DomainErrors.User.UserNotFound);
		}

		if (!await userManager.CheckPasswordAsync(user, request.Password))
		{
			return Result.Failure<JwtTokenResponse>( DomainErrors.User.InvalidPassword);
		}
		
		var result = await jwtTokenManager.GenerateJwtAndRefreshToken(request.Email);

		return result;
	}
}