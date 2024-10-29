using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CompanyManager.Application.Common.Interfaces.Application.Services;
using CompanyManager.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CompanyManager.Application.Common.Services;

public class JwtTokenGenerator : IJwtTokenGenerator
{
	private readonly string _secret;
	private readonly int _expirationTime;

	public JwtTokenGenerator(IConfiguration configuration)
	{
		_secret = configuration["Jwt:Secret"];
		_expirationTime = int.Parse(configuration["Jwt:ExpirationTime"]);
	}

	public async Task<string> GenerateToken(ApplicationUser user, List<string> roles = null,
		List<Claim> claims = null)
	{
		var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secret));
		var tokenHandler = new JwtSecurityTokenHandler();
		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = CreateClaimsIdentity(user, roles, claims),
			Expires = DateTime.UtcNow.AddSeconds(_expirationTime),
			SigningCredentials =
				new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
		};

		var token = tokenHandler.CreateToken(tokenDescriptor);
		return tokenHandler.WriteToken(token);
	}

	private ClaimsIdentity CreateClaimsIdentity(ApplicationUser user, List<string> roles, List<Claim> claims)
	{
		var claimsIdentity = new ClaimsIdentity(new[]
		{
			new Claim(ClaimTypes.Email, user.Email)
		});

		if (roles != null)
		{
			foreach (var role in roles)
			{
				claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
			}
		}

		if (claims != null)
		{
			foreach (var claim in claims)
			{
				claimsIdentity.AddClaim(claim);
			}
		}

		return claimsIdentity;
	}
}