using CompanyManager.Application.Actions.LeaveApplicationsActions.Commands.AcceptApplyForLeave;
using CompanyManager.Application.Actions.LeaveApplicationsActions.Commands.ApplyForLeave;
using CompanyManager.Application.Actions.LeaveApplicationsActions.Commands.RejectApplyForLeave;
using CompanyManager.Common.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManager.Controllers;

[Route("api/[controller]")]
public class LeaveApplicationController(ISender sender) : BaseController(sender)
{
	[HttpPost("{employeeId}")]
	[Authorize(Policy = PolicyValues.Employee)]
	public async Task<IActionResult> ApplyForLeave(Guid employeeId, [FromBody] ApplyForLeaveCommand command)
	{
		if (employeeId != command.EmployeeId)
			return BadRequest("Employee ID in the URL does not match the Employee ID in the request body.");
		
		var result = await Sender.Send(command);
		
		return result.IsSuccess
			? Ok(result.Value)
			: HandleFailure(result);
	}
	
	[HttpPut("accept/{leaveApplicationId}")]
	[Authorize(Policy = PolicyValues.CompanyOwner)]
	public async Task<IActionResult> AcceptApplyForLeave(Guid leaveApplicationId)
	{
		var command = new AcceptApplyForLeaveCommand(leaveApplicationId);
		var result = await Sender.Send(command);
		
		return result.IsSuccess
			? Ok(result.Value)
			: HandleFailure(result);
	}
	
	[HttpPut("reject/{leaveApplicationId}")]
	[Authorize(Policy = PolicyValues.CompanyOwner)]
	public async Task<IActionResult> RejectApplyForLeave(Guid leaveApplicationId)
	{
		var command = new RejectApplyForLeaveCommand(leaveApplicationId);
		var result = await Sender.Send(command);
		
		return result.IsSuccess
			? Ok(result.Value)
			: HandleFailure(result);
	}
}