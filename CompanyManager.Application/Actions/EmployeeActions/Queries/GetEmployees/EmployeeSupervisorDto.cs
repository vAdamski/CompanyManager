namespace CompanyManager.Application.Actions.EmployeeActions.Queries.GetEmployees;

public class EmployeeSupervisorDto(string firstName, string lastName)
{
	public string FullName => $"{firstName} {lastName}";
}