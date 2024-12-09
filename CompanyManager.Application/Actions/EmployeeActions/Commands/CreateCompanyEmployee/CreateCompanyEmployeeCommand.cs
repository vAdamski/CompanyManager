using CompanyManager.Application.Common.Abstraction.Messaging;

namespace CompanyManager.Application.Actions.EmployeeActions.Commands.CreateCompanyEmployee;

public class CreateCompanyEmployeeCommand : ICommand<Guid>
{
	public Guid CompanyId { get; private set; }
	public List<Guid> Supervisors { get; private set; }
	public string FirstName { get; private set; }
	public string LastName { get; private set; }
	public string Email { get; private set; }
	public string UserName { get; private set; }

	private CreateCompanyEmployeeCommand()
	{
	}

	public CreateCompanyEmployeeCommand(Guid companyId, List<Guid> supervisors, string firstName, string lastName,
		string email, string userName)
	{
		CompanyId = companyId;
		Supervisors = supervisors;
		FirstName = firstName;
		LastName = lastName;
		Email = email;
		UserName = userName;
	}
}