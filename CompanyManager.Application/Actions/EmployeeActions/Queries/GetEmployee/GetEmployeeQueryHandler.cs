using CompanyManager.Application.Common.Abstraction.Messaging;
using CompanyManager.Application.Common.Interfaces.Persistence;
using CompanyManager.Domain.Common;
using CompanyManager.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace CompanyManager.Application.Actions.EmployeeActions.Queries.GetEmployee;

public class GetEmployeeQueryHandler(IAppDbContext ctx) : IQueryHandler<GetEmployeeQuery, EmployeeDetailsViewModel>
{
	public async Task<Result<EmployeeDetailsViewModel>> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
	{
		var employee = await ctx.Employees
			.Include(x => x.Supervisors)
			.ThenInclude(x => x.Supervisor)
			.FirstOrDefaultAsync(e => e.Id == request.EmployeeId, cancellationToken);

		if (employee == null)
			return Result.Failure<EmployeeDetailsViewModel>(DomainErrors.Employee.EmployeeNotFound);

		var employeeSupervisors = employee.Supervisors.Select(s =>
			new EmployeeSupervisorDetailsDto(s.Supervisor.Id, s.Supervisor.FirstName, s.Supervisor.LastName)).ToList();

		return Result.Success(new EmployeeDetailsViewModel(employee.Id, employee.FirstName, employee.LastName,
			employee.UserName, employee.Email, employeeSupervisors));
	}
}