using CompanyManager.Application.Common.Abstraction.Messaging;

namespace CompanyManager.Application.Actions.LeaveApplicationsActions.Commands.RejectApplyForLeave;

public class RejectApplyForLeaveCommand : ICommand<Guid>
{
	public Guid LeaveApplicationId { get; private set; }

	private RejectApplyForLeaveCommand()
	{
	}

	public RejectApplyForLeaveCommand(Guid leaveApplicationId)
	{
		LeaveApplicationId = leaveApplicationId;
	}
}