using CompanyManager.Application.Common.Abstraction.Messaging;
using CompanyManager.Application.Common.Interfaces.Persistence;
using CompanyManager.Domain.Common;
using CompanyManager.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace CompanyManager.Application.Actions.LeaveApplicationsActions.Commands.AcceptApplyForLeave;

public class AcceptApplyForLeaveCommandHandler(IAppDbContext ctx) : ICommandHandler<AcceptApplyForLeaveCommand, Guid>
{
	public async Task<Result<Guid>> Handle(AcceptApplyForLeaveCommand request, CancellationToken cancellationToken)
	{
		var leaveApplication = await ctx.LeaveApplications
			.FirstOrDefaultAsync(x => x.Id == request.LeaveApplicationId, cancellationToken);
		
		if (leaveApplication == null)
			return Result.Failure<Guid>(DomainErrors.LeaveApplication.LeaveApplicationNotFound);
		
		var result = leaveApplication.Accept();
		
		if (result.IsFailure)
			return Result.Failure<Guid>(result.Error);
		
		await ctx.SaveChangesAsync(cancellationToken);
		
		return Result.Success(leaveApplication.Id);
	}
}