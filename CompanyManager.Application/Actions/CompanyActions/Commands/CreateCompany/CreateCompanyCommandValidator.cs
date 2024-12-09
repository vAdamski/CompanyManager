using CompanyManager.Application.Common.CustomValidators;
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
			.SetValidator(new EmailIsTakenValidator(ctx));

		RuleFor(x => x.CompanyName)
			.NotEmpty()
			.WithMessage("Company name cannot be empty");
		
		RuleFor(x => x.CompanyOwnerFirstName)
			.NotEmpty()
			.WithMessage("Company owner first name cannot be empty");

		RuleFor(x => x.CompanyOwnerLastName)
			.NotEmpty()
			.WithMessage("Company owner last name cannot be empty");
		
		RuleFor(x => x.CompanyOwnerUserName)
			.NotEmpty()
			.WithMessage("Company owner username cannot be empty");
	}
}