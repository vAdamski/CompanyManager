using CompanyManager.Application.Actions.EmployeeActions.Commands.CreateCompanyEmployee;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManager.Controllers;

[Route("api/[controller]")]
public class EmployeesController(ISender sender) : BaseController(sender)
{
	[HttpPost]
	[Authorize(Policy = "CompanyOwner")]
	public async Task<IActionResult> CreateCompanyEmployee([FromBody] CreateCompanyEmployeeCommand command)
	{
		var result = await Sender.Send(command);
		
		return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
	}
}