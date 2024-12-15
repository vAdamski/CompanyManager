using CompanyManager.Application.Common.Abstraction.Messaging;
using CompanyManager.Domain.Enums;

namespace CompanyManager.Application.Actions.EmployeeSchoolsActions.Commands.CreateSchoolForEmployee;

public class CreateSchoolForEmployeeCommand : ICommand<Guid>
{
	public Guid EmployeeId { get; private set; }
	public string SchoolName { get; private set; }
	public SchoolType SchoolType { get; private set; }
	public DateOnly StartDate { get; private set; }
	public DateOnly? EndDate { get; private set; }
	
	private CreateSchoolForEmployeeCommand()
	{
		
	}

	public CreateSchoolForEmployeeCommand(Guid employeeId, string schoolName, SchoolType schoolType, DateOnly startDate, DateOnly? endDate)
	{
		EmployeeId = employeeId;
		SchoolName = schoolName;
		SchoolType = schoolType;
		StartDate = startDate;
		EndDate = endDate;
	}
}