using CompanyManager.Application.Common.Abstraction.Messaging;

namespace CompanyManager.Application.Actions.LeaveApplicationsActions.Commands.AcceptApplyForLeave;

public class AcceptApplyForLeaveCommand : ICommand<Guid>
{
	public Guid LeaveApplicationId { get; private set; }

	private AcceptApplyForLeaveCommand()
	{
	}

	public AcceptApplyForLeaveCommand(Guid leaveApplicationId)
	{
		LeaveApplicationId = leaveApplicationId;
	}
}