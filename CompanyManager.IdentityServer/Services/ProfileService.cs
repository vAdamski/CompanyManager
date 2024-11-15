using System.Security.Claims;
using CompanyManager.IdentityServer.Models;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using Microsoft.AspNetCore.Identity;

namespace CompanyManager.IdentityServer.Services;

public class ProfileService(UserManager<ApplicationUser> userMgr) : IProfileService
{
	public async Task GetProfileDataAsync(ProfileDataRequestContext context)
	{
		var user = await userMgr.GetUserAsync(context.Subject);
		
		var claims = new List<Claim>()
		{
			new Claim("Email", user.Email),
			new Claim("name", user.UserName),
			new Claim("firstName", user.FirstName),
			new Claim("lastName", user.LastName)
		};

		var roles = await userMgr.GetRolesAsync(user);

		foreach (var role in roles)
		{
			claims.Add(new Claim(JwtClaimTypes.Role, role));
		}

		context.IssuedClaims.AddRange(claims);
	}

	public async Task IsActiveAsync(IsActiveContext context)
	{
		var user = await userMgr.GetUserAsync(context.Subject);
		context.IsActive = user != null;
	}
}