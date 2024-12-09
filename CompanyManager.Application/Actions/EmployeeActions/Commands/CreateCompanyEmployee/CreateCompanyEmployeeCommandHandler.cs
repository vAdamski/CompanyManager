using CompanyManager.Application.Common.Abstraction.Messaging;
using CompanyManager.Application.Common.Interfaces.Persistence;
using CompanyManager.Domain.Common;
using CompanyManager.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace CompanyManager.Application.Actions.EmployeeActions.Commands.CreateCompanyEmployee;

public class CreateCompanyEmployeeCommandHandler(IAppDbContext ctx)
	: ICommandHandler<CreateCompanyEmployeeCommand, Guid>
{
	public async Task<Result<Guid>> Handle(CreateCompanyEmployeeCommand request, CancellationToken cancellationToken)
	{
		var company = await ctx.Companies
			.Include(x => x.Employees)
			.FirstOrDefaultAsync(x => x.Id == request.CompanyId, cancellationToken);

		if (company is null)
			return Result.Failure<Guid>(DomainErrors.Company.CompanyNotFound);
		
		var listOfSupervisors = company.Employees
			.Where(x => request.Supervisors.Contains(x.Id))
			.ToList();

		var result = company.CreateEmployee(
			request.FirstName,
			request.LastName,
			request.Email,
			request.UserName,
			listOfSupervisors);

		if (result.IsFailure)
			return Result.Failure<Guid>(result.Error);

		await ctx.SaveChangesAsync(cancellationToken);

		return Result.Success(result.Value.Id);
	}
}