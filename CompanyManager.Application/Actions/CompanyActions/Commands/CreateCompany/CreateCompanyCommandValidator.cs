using CompanyManager.Application.Common.Interfaces.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CompanyManager.Application.Actions.CompanyActions.Commands.CreateCompany;

public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
{
	public CreateCompanyCommandValidator(IAppDbContext ctx)
	{
		RuleFor(x => x.CompanyOwnerEmail)
			.EmailAddress()
			.WithMessage("Invalid email address")
			.MustAsync(async (email, cancellationToken) =>
			{
				return await ctx.Employees.AllAsync(x => x.Email != email, cancellationToken);
			})
			.WithMessage("Company with this email already exists");
	}
}