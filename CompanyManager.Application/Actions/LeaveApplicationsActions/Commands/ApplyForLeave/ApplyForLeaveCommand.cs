using CompanyManager.Application.Common.Abstraction.Messaging;
using CompanyManager.Domain.Enums;

namespace CompanyManager.Application.Actions.LeaveApplicationsActions.Commands.ApplyForLeave;

public class ApplyForLeaveCommand : ICommand<Guid>
{
	public Guid EmployeeId { get; private set; }
	public DateOnly FreeFrom { get; private set; }
	public DateOnly FreeTo { get; private set; }
	public int DaysOff { get; private set; }
	public LeaveApplicationType Type { get; private set; }
	
	private ApplyForLeaveCommand()
	{
		
	}
	
	public ApplyForLeaveCommand(Guid employeeId, DateOnly freeFrom, DateOnly freeTo, int daysOff, LeaveApplicationType type)
	{
		EmployeeId = employeeId;
		FreeFrom = freeFrom;
		FreeTo = freeTo;
		DaysOff = daysOff;
		Type = type;
	}
}