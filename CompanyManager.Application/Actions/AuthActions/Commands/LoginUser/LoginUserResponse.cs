namespace CompanyManager.Application.Actions.AuthActions.Commands.LoginUser;

public class LoginUserResponse(string jwtToken, string refreshToken)
{
	public string JwtToken { get; set; } = jwtToken;
	public string RefreshToken { get; set; } = refreshToken;
}