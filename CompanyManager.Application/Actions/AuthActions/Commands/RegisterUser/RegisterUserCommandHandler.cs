using CompanyManager.Application.Common.Abstraction.Messaging;
using CompanyManager.Application.Common.Interfaces.Persistence;
using CompanyManager.Domain.Common;
using CompanyManager.Domain.Entities;
using CompanyManager.Domain.Errors;
using Microsoft.AspNetCore.Identity;

namespace CompanyManager.Application.Actions.AuthActions.Commands.RegisterUser;

public class RegisterUserCommandHandler(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork) : ICommandHandler<RegisterUserCommand>
{
	public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
	{
		await unitOfWork.BeginTransactionAsync();
		
		var existingUser = await userManager.FindByEmailAsync(request.Email);
		if (existingUser != null)
		{
			return Result.Failure(DomainErrors.User.EmailAlreadyInUse);
		}

		var user = new ApplicationUser
		{
			UserName = request.Email,
			Email = request.Email,
		};
		
		var result = await userManager.CreateAsync(user, request.Password);
		
		if (!result.Succeeded)
		{
			await unitOfWork.RollbackTransactionAsync();
			return Result.Failure(DomainErrors.User.RegisterFailed);
		}

		await unitOfWork.CommitTransactionAsync();

		return Result.Success();
	}
}