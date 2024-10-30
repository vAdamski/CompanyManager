using CompanyManager.Application.Common.Abstraction.Messaging;
using CompanyManager.Application.Common.Interfaces.Application.Managers;
using CompanyManager.Application.Common.Models;
using CompanyManager.Domain.Common;

namespace CompanyManager.Application.Actions.AuthActions.Commands.RefreshToken;

public class RefreshTokenCommandHandler(IJwtTokenManager jwtTokenManager)
	: ICommandHandler<RefreshTokenCommand, JwtTokenResponse>
{
	public async Task<Result<JwtTokenResponse>> Handle(RefreshTokenCommand request,
		CancellationToken cancellationToken)
	{
		var result = await jwtTokenManager.RefreshToken(request.JwtToken, request.RefreshToken);
		
		return result;
	}
}