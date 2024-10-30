namespace CompanyManager.Application.Common.Models;

public class JwtTokenResponse
{
	private JwtTokenResponse(string jwtToken, string refreshToken, bool isLoggedIn = false)
	{
		JwtToken = jwtToken;
		RefreshToken = refreshToken;
		IsLoggedIn = isLoggedIn;
	}
	
	public string JwtToken { get; }
	public string RefreshToken { get; }
	public bool IsLoggedIn { get; }
	
	public static JwtTokenResponse Success(string jwtToken, string refreshToken)
	{
		return new JwtTokenResponse(jwtToken, refreshToken, true);
	}
	
	public static JwtTokenResponse Failed()
	{
		return new JwtTokenResponse(string.Empty, string.Empty, false);
	}
}