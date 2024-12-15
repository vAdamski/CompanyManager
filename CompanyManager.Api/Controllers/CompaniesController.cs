using CompanyManager.Application.Actions.CompanyActions.Commands.CreateCompany;
using CompanyManager.Application.Actions.CompanyActions.Queries.GetCompanyDetailsForCurrentUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManager.Controllers;

[Route("api/[controller]")]
public class CompaniesController(ISender sender) : BaseController(sender)
{
	[HttpGet("current")]
	public async Task<IActionResult> GetCompany()
	{
		var result = await Sender.Send(new GetCompanyDetailsForCurrentUserQuery());
		
		return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
	}
	
	[HttpPost]
	[Authorize(Policy = "Admin")]
	public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyCommand command)
	{
		var result = await Sender.Send(command);
		
		return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
	}
}