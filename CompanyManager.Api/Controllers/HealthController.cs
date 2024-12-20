using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : BaseController
{
	public HealthController(ISender sender) : base(sender)
	{
	}
	
	[AllowAnonymous]
	[HttpGet]
	public async Task<IActionResult> Get()
	{
		return Ok("Healthy");
	}
	
	[Authorize]
	[HttpGet("authorize")]
	public async Task<IActionResult> Authorize()
	{
		return Ok("Authorized");
	}
	
	[Authorize(Policy = "User")]
	[HttpGet("policy-user")]
	public async Task<IActionResult> PolicyUser()
	{
		return Ok("Policy User");
	}
	
	[HttpGet("policy-admin")]
	[Authorize(Policy = "Admin")]
	public async Task<IActionResult> PolicyAdmin()
	{
		return Ok("Policy Admin");
	}
	
	[Authorize(Policy = "ApiScope")]
	[HttpGet("policy-api-scope")]
	public async Task<IActionResult> PolicyApiScope()
	{
		return Ok("Policy ApiScope");
	}
}