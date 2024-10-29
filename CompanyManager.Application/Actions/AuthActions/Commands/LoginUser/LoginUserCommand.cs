using CompanyManager.Application.Common.Abstraction.Messaging;

namespace CompanyManager.Application.Actions.AuthActions.Commands.LoginUser;

public class LoginUserCommand : ICommand<LoginUserResponse>
{
	public LoginUserCommand(string email, string password)
	{
		if (string.IsNullOrWhiteSpace(email))
			throw new ArgumentNullException(nameof(email));
		
		if (string.IsNullOrWhiteSpace(password))
			throw new ArgumentNullException(nameof(password));
		
		Email = email;
		Password = password;
	}

	public string Email { get; }
	public string Password { get; }
}