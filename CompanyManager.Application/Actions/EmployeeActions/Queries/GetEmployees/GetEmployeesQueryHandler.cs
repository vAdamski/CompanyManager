using CompanyManager.Application.Common.Abstraction.Messaging;
using CompanyManager.Application.Common.Interfaces.Persistence;
using CompanyManager.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CompanyManager.Application.Actions.EmployeeActions.Queries.GetEmployees;

public class GetEmployeesQueryHandler(IAppDbContext ctx) : IQueryHandler<GetEmployeesQuery, EmployeesListViewModel>
{
	public async Task<Result<EmployeesListViewModel>> Handle(GetEmployeesQuery request,
		CancellationToken cancellationToken)
	{
		var employees = await ctx.Employees
			.Include(x => x.Supervisors)
			.ThenInclude(x => x.Supervisor)
			.Where(e => e.CompanyId == request.CompanyId)
			.Select(e => new EmployeeListItem(e.Id, e.FirstName, e.LastName,
				e.Supervisors.Select(s => new EmployeeSupervisorDto(s.Supervisor.FirstName, s.Supervisor.LastName)).ToList()))
			.ToListAsync(cancellationToken);
		
		return Result.Success(new EmployeesListViewModel(employees));
	}
}