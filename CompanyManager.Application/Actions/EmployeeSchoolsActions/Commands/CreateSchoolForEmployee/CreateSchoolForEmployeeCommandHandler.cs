using CompanyManager.Application.Common.Abstraction.Messaging;
using CompanyManager.Application.Common.Interfaces.Persistence;
using CompanyManager.Domain.Common;
using CompanyManager.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace CompanyManager.Application.Actions.EmployeeSchoolsActions.Commands.CreateSchoolForEmployee;

public class CreateSchoolForEmployeeCommandHandler(IAppDbContext ctx) : ICommandHandler<CreateSchoolForEmployeeCommand, Guid>
{
	public async Task<Result<Guid>> Handle(CreateSchoolForEmployeeCommand request, CancellationToken cancellationToken)
	{
		var employee = await ctx.Employees
			.Include(x => x.Schools)
			.FirstOrDefaultAsync(x => x.Id == request.EmployeeId, cancellationToken);
		
		if (employee == null)
			return Result.Failure<Guid>(DomainErrors.Employee.EmployeeNotFound);

		var result = employee.AddSchoolInfo(request.SchoolName, request.StartDate, request.EndDate, request.SchoolType);
		
		if (result.IsFailure)
			return Result.Failure<Guid>(result.Error);
		
		await ctx.SaveChangesAsync(cancellationToken);
		
		return Result.Success(result.Value.Id);
	}
}