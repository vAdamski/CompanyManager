using CompanyManager.Application.Common.Abstraction.Messaging;

namespace CompanyManager.Application.Actions.EmployeeActions.Queries.GetEmployee;

public class GetEmployeeQuery(Guid employeeId) : IQuery<EmployeeDetailsViewModel>
{
	public Guid EmployeeId { get; private set; } = employeeId;
}