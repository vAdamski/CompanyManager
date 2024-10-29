using CompanyManager.Application.Common.Abstraction.Messaging;

namespace CompanyManager.Application.Actions.AuthActions.Commands.RegisterUser;

public class RegisterUserCommand : ICommand
{
	public RegisterUserCommand(string email, string password, string role = "User")
	{
		if (string.IsNullOrWhiteSpace(email))
		{
			throw new ArgumentException("Email is required", nameof(email));
		}
		
		if (string.IsNullOrWhiteSpace(password))
		{
			throw new ArgumentException("Password is required", nameof(password));
		}
		
		Email = email;
		Password = password;
		Role = role;
	}

	public string Email { get; }
	public string Password { get; }
	public string Role { get; }
}