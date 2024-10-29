using Microsoft.AspNetCore.Identity;

namespace CompanyManager.Domain.Entities;

public class ApplicationUser : IdentityUser
{
	private ApplicationUser()
	{
		
	}
	
	public static ApplicationUser Create(string email)
	{
		return new ApplicationUser
		{
			Email = email,
			UserName = email
		};
	}

	public string? RefreshToken { get; set; }
	public DateTime? RefreshTokenExpiration { get; set; }

	public void SetRefreshToken(string refreshToken, DateTime refreshTokenExpiration)
	{
		RefreshToken = refreshToken;
		RefreshTokenExpiration = refreshTokenExpiration;
	}
}