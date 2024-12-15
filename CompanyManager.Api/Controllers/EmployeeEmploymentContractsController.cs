using CompanyManager.Application.Actions.EmployeeEmploymentContractActions.Commands.CreateEmployeeEmploymentContract;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManager.Controllers;

[Route("api/[controller]")]
public class EmployeeEmploymentContractsController(ISender sender) : BaseController(sender)
{
	[HttpPost("{employeeId}")]
	[Authorize(Policy = "CompanyOwner")]
	public async Task<IActionResult> CreateEmployeeEmploymentContract(Guid employeeId,
		[FromBody] CreateEmployeeEmploymentContractCommand command)
	{
		if (employeeId != command.EmployeeId)
			return BadRequest("Employee ID in the URL does not match the Employee ID in the request body.");
		
		var result = await Sender.Send(command);

		return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
	}
}