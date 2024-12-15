using FluentValidation;

namespace CompanyManager.Application.Actions.EmployeeEmploymentContractActions.Commands.
	CreateEmployeeEmploymentContract;

public class
	CreateEmployeeEmploymentContractCommandValidator : AbstractValidator<CreateEmployeeEmploymentContractCommand>
{
	public CreateEmployeeEmploymentContractCommandValidator()
	{
		RuleFor(x => x.EmployeeId)
			.NotEmpty()
			.WithMessage("Employee ID is required.");
		RuleFor(x => x.CompanyName)
			.NotEmpty()
			.WithMessage("Company name is required.");
		RuleFor(x => x.StartDate)
			.NotEmpty()
			.WithMessage("Start date is required.");
		RuleFor(x => x.EndDate)
			.GreaterThanOrEqualTo(x => x.StartDate)
			.When(x => x.EndDate.HasValue)
			.WithMessage("End date cannot be earlier than start date.");
	}
}