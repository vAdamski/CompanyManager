using CompanyManager.Application.Common.Abstraction.Messaging;

namespace CompanyManager.Application.Actions.EmployeeEmploymentContractActions.Commands.CreateEmployeeEmploymentContract;

public class CreateEmployeeEmploymentContractCommand : ICommand<Guid>
{
	private CreateEmployeeEmploymentContractCommand()
	{
		
	}
	
	public CreateEmployeeEmploymentContractCommand(Guid employeeId, string companyName, DateOnly startDate, DateOnly? endDate)
	{
		EmployeeId = employeeId;
		CompanyName = companyName;
		StartDate = startDate;
		EndDate = endDate;
	}

	public Guid EmployeeId { get; private set; }
	public string CompanyName { get; private set; }
	public DateOnly StartDate { get; private set; }
	public DateOnly? EndDate { get; private set; }
}