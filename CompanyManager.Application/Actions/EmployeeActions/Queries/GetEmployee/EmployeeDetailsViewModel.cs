using CompanyManager.Domain.Entities;

namespace CompanyManager.Application.Actions.EmployeeActions.Queries.GetEmployee;

public class EmployeeDetailsViewModel(Guid id, string firstName, string lastName, string userName, string email, List<EmployeeSupervisorDetailsDto> employeeSupervisors)
{
	public Guid Id { get; } = id;
	public string FirstName { get; } = firstName;
	public string LastName { get; } = lastName;
	public string UserName { get; } = userName;
	public string Email { get; } = email;
	
	public List<EmployeeSupervisorDetailsDto> EmployeeSupervisors { get; } = employeeSupervisors;
}