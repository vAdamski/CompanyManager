using CompanyManager.Application.Common.Abstraction.Messaging;
using CompanyManager.Application.Common.Models;

namespace CompanyManager.Application.Actions.AuthActions.Commands.RefreshToken;

public class RefreshTokenCommand(RefreshTokenRequestModel refreshTokenRequestModel) : ICommand<JwtTokenResponse>
{
	public string JwtToken { get; } = refreshTokenRequestModel.JwtToken;
	public string RefreshToken { get; } = refreshTokenRequestModel.RefreshToken;
}