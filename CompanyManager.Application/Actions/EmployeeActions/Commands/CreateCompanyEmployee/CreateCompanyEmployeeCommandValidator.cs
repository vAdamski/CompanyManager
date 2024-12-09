using CompanyManager.Application.Common.CustomValidators;
using CompanyManager.Application.Common.Interfaces.Persistence;
using FluentValidation;

namespace CompanyManager.Application.Actions.EmployeeActions.Commands.CreateCompanyEmployee;

public class CreateCompanyEmployeeCommandValidator : AbstractValidator<CreateCompanyEmployeeCommand>
{
	public CreateCompanyEmployeeCommandValidator(IAppDbContext ctx)
	{
		RuleFor(x => x.CompanyId)
			.NotEmpty();
		RuleFor(x => x.Supervisors)
			.NotEmpty();
		RuleFor(x => x.FirstName)
			.NotEmpty();
		RuleFor(x => x.LastName)
			.NotEmpty();
		RuleFor(x => x.Email)
			.NotEmpty()
			.EmailAddress()
			.SetValidator(new EmailIsTakenValidator(ctx));
		RuleFor(x => x.UserName)
			.NotEmpty();
	}
}