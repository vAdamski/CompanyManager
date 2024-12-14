namespace CompanyManager.Application.Actions.EmployeeActions.Queries.GetEmployees;

public class EmployeeListItem(Guid id,string firstName, string lastName, List<EmployeeSupervisorDto> employeeSupervisors)
{
	public Guid Id { get; } = id;
	public string FirstName { get; } = firstName;
	public string LastName { get; } = lastName;
	public List<EmployeeSupervisorDto> EmployeeSupervisors { get; } = employeeSupervisors;
}