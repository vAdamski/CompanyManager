namespace CompanyManager.Application.Common.Interfaces.Application.Services;

public interface IRefreshTokenGenerator
{
	string GenerateRefreshToken();
	DateTime GetRefreshTokenExpiry();
}