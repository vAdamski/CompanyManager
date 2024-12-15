using CompanyManager.Application.Common.Abstraction.Messaging;
using CompanyManager.Application.Common.Interfaces.Persistence;
using CompanyManager.Domain.Common;
using CompanyManager.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace CompanyManager.Application.Actions.LeaveApplicationsActions.Commands.RejectApplyForLeave;

public class RejectApplyForLeaveCommandHandler(IAppDbContext ctx) : ICommandHandler<RejectApplyForLeaveCommand, Guid>
{
	public async Task<Result<Guid>> Handle(RejectApplyForLeaveCommand request, CancellationToken cancellationToken)
	{
		var leaveApplication = await ctx.LeaveApplications
			.FirstOrDefaultAsync(x => x.Id == request.LeaveApplicationId, cancellationToken);
		
		if (leaveApplication == null)
			return Result.Failure<Guid>(DomainErrors.LeaveApplication.LeaveApplicationNotFound);
		
		var result = leaveApplication.Reject();
		
		if (result.IsFailure)
			return Result.Failure<Guid>(result.Error);
		
		await ctx.SaveChangesAsync(cancellationToken);
		
		return Result.Success(leaveApplication.Id);
	}
}