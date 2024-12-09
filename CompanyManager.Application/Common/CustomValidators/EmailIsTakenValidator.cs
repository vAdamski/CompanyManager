using CompanyManager.Application.Common.Interfaces.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CompanyManager.Application.Common.CustomValidators;

public class EmailIsTakenValidator : AbstractValidator<string>
{
	public EmailIsTakenValidator(IAppDbContext ctx)
	{
		RuleFor(email => email)
			.MustAsync(async (email, cancellationToken) =>
			{
				return await ctx.Employees.AllAsync(x => x.Email != email, cancellationToken);
			})
			.WithMessage("Company with this email already exists");
	}
}