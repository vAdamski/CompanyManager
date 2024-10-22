using CompanyManager.Application.Common.Abstraction.Messaging;

namespace CompanyManager.Application.Actions.AuthActions.Commands.RegisterUser;

public class RegisterUserCommand(string email, string password) : ICommand
{
	public string Email { get; } = email;
	public string Password { get; } = password;
}