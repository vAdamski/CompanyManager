using CompanyManager.Application.Actions.EmployeeSchoolsActions.Commands.CreateSchoolForEmployee;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManager.Controllers;

[Route("api/[controller]")]
public class EmployeeSchoolsController(ISender sender) : BaseController(sender)
{
	[HttpPost("{employeeId}")]
	public async Task<IActionResult> CreateSchoolForEmployee(Guid employeeId,
		[FromBody] CreateSchoolForEmployeeCommand command)
	{
		if (employeeId != command.EmployeeId)
			return BadRequest("Employee ID in the route and in the body do not match.");

		var result = await Sender.Send(command);

		return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
	}
}