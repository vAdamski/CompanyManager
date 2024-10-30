namespace CompanyManager.Application.Actions.AuthActions.Commands.RefreshToken;

public class RefreshTokenRequestModel
{
	public string JwtToken { get; set; } 
	public string RefreshToken { get; set; }
}