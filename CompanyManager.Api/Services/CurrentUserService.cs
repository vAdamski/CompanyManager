using System.Security.Claims;
using CompanyManager.Application.Common.Interfaces.Api.Services;

namespace CompanyManager.Services;

public class CurrentUserService : ICurrentUserService
{
	public string Email { get; set; }
	public bool IsAuthenticated { get; set; }

	public CurrentUserService(IHttpContextAccessor httpContextAccessor)
	{
		Email = httpContextAccessor.HttpContext?.User?.FindFirstValue("Email");

		IsAuthenticated = !string.IsNullOrEmpty(Email);
	}
}