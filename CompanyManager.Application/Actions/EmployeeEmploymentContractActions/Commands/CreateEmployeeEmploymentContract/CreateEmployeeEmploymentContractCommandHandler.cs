using CompanyManager.Application.Common.Abstraction.Messaging;
using CompanyManager.Application.Common.Interfaces.Persistence;
using CompanyManager.Domain.Common;
using CompanyManager.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace CompanyManager.Application.Actions.EmployeeEmploymentContractActions.Commands.CreateEmployeeEmploymentContract;

public class CreateEmployeeEmploymentContractCommandHandler(IAppDbContext ctx) : ICommandHandler<CreateEmployeeEmploymentContractCommand, Guid>
{
	public async Task<Result<Guid>> Handle(CreateEmployeeEmploymentContractCommand request, CancellationToken cancellationToken)
	{
		var employee = await ctx.Employees
			.Include(x => x.EmploymentContracts)
			.FirstOrDefaultAsync(x => x.Id == request.EmployeeId, cancellationToken);

		if (employee == null)
			return Result.Failure<Guid>(DomainErrors.Employee.EmployeeNotFound);
		
		var result = employee.AddEmploymentContract(request.CompanyName, request.StartDate, request.EndDate);

		if (result.IsFailure)
			return Result.Failure<Guid>(result.Error);
		
		await ctx.SaveChangesAsync(cancellationToken);
		
		return Result.Success(result.Value.Id);
	}
}