using CompanyManager.Application.Actions.CompanyActions.Commands.CreateCompany;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManager.Controllers;

[Route("api/[controller]")]
public class CompaniesController(ISender sender) : BaseController(sender)
{
	[HttpPost]
	public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyCommand command)
	{
		var result = await Sender.Send(command);
		
		return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
	}
}