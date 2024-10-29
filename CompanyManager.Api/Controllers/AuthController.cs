using CompanyManager.Application.Actions.AuthActions.Commands.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(ISender sender) : BaseController(sender)
{
	[HttpPost("register")]
	public async Task<IActionResult> Register(string email, string password)
	{
		var result = await sender.Send(new RegisterUserCommand(email, password));
		
		return Ok(result);
	}
}