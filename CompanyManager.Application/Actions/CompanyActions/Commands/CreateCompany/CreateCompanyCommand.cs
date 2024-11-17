using CompanyManager.Application.Common.Abstraction.Messaging;

namespace CompanyManager.Application.Actions.CompanyActions.Commands.CreateCompany;

public class CreateCompanyCommand : ICommand<Guid>
{
	public string CompanyName { get; private set; }
	public string CompanyOwnerFirstName { get; private set; }
	public string CompanyOwnerLastName { get; private set; }
	public string CompanyOwnerEmail { get; private set; }
	public string CompanyOwnerUserName { get; private set; }

	private CreateCompanyCommand()
	{
		
	}

	public CreateCompanyCommand(string companyName, string companyOwnerFirstName, string companyOwnerLastName,
		string companyOwnerEmail, string companyOwnerUserName)
	{
		CompanyName = companyName;
		CompanyOwnerFirstName = companyOwnerFirstName;
		CompanyOwnerLastName = companyOwnerLastName;
		CompanyOwnerEmail = companyOwnerEmail;
		CompanyOwnerUserName = companyOwnerUserName;
	}
}