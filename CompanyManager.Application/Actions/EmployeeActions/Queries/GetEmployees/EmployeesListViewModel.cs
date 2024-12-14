namespace CompanyManager.Application.Actions.EmployeeActions.Queries.GetEmployees;

public class EmployeesListViewModel(List<EmployeeListItem> employees)
{
	public List<EmployeeListItem> Employees { get; } = employees;
}