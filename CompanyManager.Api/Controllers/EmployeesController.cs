using CompanyManager.Application.Actions.EmployeeActions.Commands.CreateCompanyEmployee;
using CompanyManager.Application.Actions.EmployeeActions.Commands.UpdateCompanyEmployee;
using CompanyManager.Application.Actions.EmployeeActions.Queries.GetEmployee;
using CompanyManager.Application.Actions.EmployeeActions.Queries.GetEmployees;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManager.Controllers;

[Route("api/[controller]")]
public class EmployeesController(ISender sender) : BaseController(sender)
{
	[HttpGet]
	[Authorize(Policy = "CompanyOwner")]
	public async Task<IActionResult> GetCompanyEmployees([FromQuery] Guid companyId)
	{
		var result = await Sender.Send(new GetEmployeesQuery(companyId));
		return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
	}
	
	[HttpGet("{employeeId}")]
	[Authorize(Policy = "CompanyOwner")]
	public async Task<IActionResult> GetCompanyEmployee(Guid employeeId)
	{
		var result = await Sender.Send(new GetEmployeeQuery(employeeId));
		return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
	}
	
	[HttpPost]
	[Authorize(Policy = "CompanyOwner")]
	public async Task<IActionResult> CreateCompanyEmployee([FromBody] CreateCompanyEmployeeCommand command)
	{
		var result = await Sender.Send(command);
		
		return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
	}
	
	[HttpPut("{employeeId}")]
	[Authorize(Policy = "CompanyOwner")]
	public async Task<IActionResult> UpdateCompanyEmployee(Guid employeeId, [FromBody] UpdateCompanyEmployeeCommand command)
	{
		if (employeeId != command.EmployeeId)
			return BadRequest("Employee ID mismatch.");
		
		var result = await Sender.Send(command);

		return result.IsSuccess ? Ok() : HandleFailure(result);
		
		throw new NotImplementedException();
	}
	
	[HttpDelete("{employeeId}")]
	[Authorize(Policy = "CompanyOwner")]
	public async Task<IActionResult> DeleteCompanyEmployee(Guid employeeId)
	{
		throw new NotImplementedException();
	}
}