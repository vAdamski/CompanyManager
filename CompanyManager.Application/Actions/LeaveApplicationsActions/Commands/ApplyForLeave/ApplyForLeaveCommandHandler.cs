using CompanyManager.Application.Common.Abstraction.Messaging;
using CompanyManager.Application.Common.Interfaces.Persistence;
using CompanyManager.Domain.Common;
using CompanyManager.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace CompanyManager.Application.Actions.LeaveApplicationsActions.Commands.ApplyForLeave;

public class ApplyForLeaveCommandHandler(IAppDbContext appDbContext) : ICommandHandler<ApplyForLeaveCommand, Guid>
{
	public async Task<Result<Guid>> Handle(ApplyForLeaveCommand request, CancellationToken cancellationToken)
	{
		var employee = await appDbContext.Employees
			.FirstOrDefaultAsync(x => x.Id == request.EmployeeId, cancellationToken);
		
		if (employee == null)
			return Result.Failure<Guid>(DomainErrors.Employee.EmployeeNotFound);
		
		var result = employee.AddLeaveApplication(request.FreeFrom, request.FreeTo, request.DaysOff, request.Type);
		
		if (result.IsFailure)
			return Result.Failure<Guid>(result.Error);
		
		await appDbContext.SaveChangesAsync(cancellationToken);
		
		return Result.Success(result.Value.Id);
	}
}