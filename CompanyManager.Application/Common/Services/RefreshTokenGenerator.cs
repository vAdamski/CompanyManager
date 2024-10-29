using System.Security.Cryptography;
using CompanyManager.Application.Common.Interfaces.Application.Services;
using CompanyManager.Application.Common.Interfaces.Infrastructure.Services;
using Microsoft.Extensions.Configuration;

namespace CompanyManager.Application.Common.Services;

public class RefreshTokenGenerator(IConfiguration configuration, IDateTime dateTime) : IRefreshTokenGenerator
{
	private readonly int _refreshTokenExpiration = int.Parse(configuration["Jwt:RefreshTokenExpiration"]);

	public string GenerateRefreshToken()
	{
		var randomNumber = new byte[32];
		using var rng = RandomNumberGenerator.Create();
		rng.GetBytes(randomNumber);
		return Convert.ToBase64String(randomNumber);
	}

	public DateTime GetRefreshTokenExpiry()
	{
		return dateTime.Now.AddSeconds(_refreshTokenExpiration);
	}
}