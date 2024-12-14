using CompanyManager.Application.Common.Abstraction.Messaging;

namespace CompanyManager.Application.Actions.EmployeeActions.Queries.GetEmployees;

public class GetEmployeesQuery(Guid companyId) : IQuery<EmployeesListViewModel>
{
	public Guid CompanyId { get; private set; } = companyId;
}