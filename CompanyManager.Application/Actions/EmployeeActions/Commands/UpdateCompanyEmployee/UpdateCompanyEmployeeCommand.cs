using CompanyManager.Application.Common.Abstraction.Messaging;

namespace CompanyManager.Application.Actions.EmployeeActions.Commands.UpdateCompanyEmployee;

public class UpdateCompanyEmployeeCommand : ICommand
{
	private UpdateCompanyEmployeeCommand(string email)
	{
		Email = email;
	}

	public UpdateCompanyEmployeeCommand(Guid employeeId, string firstName, string lastName,
		string userName, string email, List<Guid> supervisors)
	{
		EmployeeId = employeeId;
		FirstName = firstName;
		LastName = lastName;
		UserName = userName;
		Supervisors = supervisors;
		Email = email;
	}

	public Guid EmployeeId { get; private set; }
	public string FirstName { get; private set; }
	public string LastName { get; private set; }
	public string UserName { get; private set; }
	public string Email { get; private set; }
	public List<Guid> Supervisors { get; private set; } = new();
}