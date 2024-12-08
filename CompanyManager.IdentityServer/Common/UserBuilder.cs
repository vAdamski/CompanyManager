using System.Security.Claims;
using CompanyManager.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace CompanyManager.IdentityServer.Common;

public class UserBuilder
{
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly RoleManager<IdentityRole> _roleManager;

	public UserBuilder(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
	{
		_userManager = userManager;
		_roleManager = roleManager;
	}

	public async Task<ApplicationUser> CreateUserAsync(
		string firstName,
		string lastName,
		string email,
		string password,
		List<string>? roles = null,
		List<string>? claims = null)
	{
		var user = await _userManager.FindByEmailAsync(email);
		if (user == null)
		{
			user = new ApplicationUser
			{
				FirstName = firstName,
				LastName = lastName,
				UserName = email,
				Email = email,
				EmailConfirmed = true
			};
			var result = await _userManager.CreateAsync(user, password);
			if (!result.Succeeded)
			{
				throw new Exception(result.Errors.First().Description);
			}

			foreach (var role in roles)
			{
				await EnsureRoleAsync(role);

				var roleAddResult = await _userManager.AddToRoleAsync(user, role);
				if (!roleAddResult.Succeeded)
				{
					throw new Exception(roleAddResult.Errors.First().Description);
				}
			}

			if (claims != null)
			{
				var claimsToAdd = claims.Select(c => new Claim(c, c)).ToList();
				var claimsAddResult = await _userManager.AddClaimsAsync(user, claimsToAdd);
				if (!claimsAddResult.Succeeded)
				{
					throw new Exception(claimsAddResult.Errors.First().Description);
				}
			}

			Log.Debug($"{firstName} {lastName} created");
		}
		else
		{
			Log.Debug($"{firstName} {lastName} already exists");
		}

		return user;
	}

	public async Task EnsureRoleAsync(string roleName)
	{
		var role = await _roleManager.FindByNameAsync(roleName);
		if (role == null)
		{
			role = new IdentityRole(roleName);
			var result = await _roleManager.CreateAsync(role);
			if (!result.Succeeded)
			{
				throw new Exception(result.Errors.First().Description);
			}

			Log.Debug($"{roleName} role created");
		}
		else
		{
			Log.Debug($"{roleName} role already exists");
		}
	}
}