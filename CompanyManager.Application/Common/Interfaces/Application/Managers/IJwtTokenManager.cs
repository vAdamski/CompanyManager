using CompanyManager.Application.Common.Models;
using CompanyManager.Domain.Common;

namespace CompanyManager.Application.Common.Interfaces.Application.Managers;

public interface IJwtTokenManager
{
	Task<Result<JwtTokenResponse>> GenerateJwtAndRefreshToken(string userEmail);
	Task<Result<JwtTokenResponse>> RefreshToken(string jwtToken, string refreshToken);
}