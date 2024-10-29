using CompanyManager.Application.Common.Abstraction.Messaging;
using CompanyManager.Application.Common.Interfaces.Persistence;
using CompanyManager.Domain.Common;
using CompanyManager.Domain.Entities;
using CompanyManager.Domain.Errors;
using Microsoft.AspNetCore.Identity;

namespace CompanyManager.Application.Actions.AuthActions.Commands.RegisterUser;

public class RegisterUserCommandHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
	: ICommandHandler<RegisterUserCommand>
{
	private static readonly List<string> AvailableRoles = new() { "Admin", "User" };

	public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
	{
		if (!IsRoleValid(request.Role))
		{
			return Result.Failure(DomainErrors.Role.NotFound);
		}

		if (await IsEmailInUseAsync(request.Email))
		{
			return Result.Failure(DomainErrors.User.EmailAlreadyInUse);
		}

		var userCreationResult = await CreateUserAsync(request.Email, request.Password);
		if (!userCreationResult.IsSuccess)
		{
			return Result.Failure(userCreationResult.Error);
		}

		var assignRoleResult = await AssignRoleToUserAsync(userCreationResult.Value, request.Role);
		if (!assignRoleResult.IsSuccess)
		{
			return Result.Failure(assignRoleResult.Error);
		}

		return Result.Success();
	}

	private bool IsRoleValid(string role)
		=> string.IsNullOrEmpty(role) || AvailableRoles.Contains(role);

	private async Task<bool> IsEmailInUseAsync(string email)
	{
		var user = await userManager.FindByEmailAsync(email);
		return user != null;
	}

	private async Task<Result<ApplicationUser>> CreateUserAsync(string email, string password)
	{
		if (email == null) throw new ArgumentNullException(nameof(email));
		if (password == null) throw new ArgumentNullException(nameof(password));

		var user = ApplicationUser.Create(email);
		if (user == null)
		{
			return Result.Failure<ApplicationUser>(DomainErrors.User.RegisterFailed);
		}

		var result = await userManager.CreateAsync(user, password);

		return result.Succeeded
			? Result.Success(user)
			: Result.Failure<ApplicationUser>(DomainErrors.User.RegisterFailed);
	}

	private async Task<Result> AssignRoleToUserAsync(ApplicationUser user, string role)
	{
		var roleEntity = await roleManager.FindByNameAsync(role);
		if (roleEntity == null)
		{
			var createRoleResult = await roleManager.CreateAsync(new IdentityRole(role));
			if (!createRoleResult.Succeeded)
			{
				return Result.Failure(DomainErrors.Role.NotFound);
			}
		}

		var addToRoleResult = await userManager.AddToRoleAsync(user, role);
		return addToRoleResult.Succeeded ? Result.Success() : Result.Failure(DomainErrors.User.RegisterFailed);
	}
}