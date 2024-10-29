using CompanyManager.Application.Actions.AuthActions.Commands.LoginUser;
using Shouldly;

namespace CompanyManager.UnitTests.Application.Actions.AuthActions.Commands.LoginUser;

public class LoginUserCommandTests
{
	[Fact]
	public void Constructor_ShouldInitializeProperties()
	{
		// Arrange
		var email = "test@example.com";
		var password = "password";

		// Act
		var command = new LoginUserCommand(email, password);

		// Assert
		command.Email.ShouldBe(email);
		command.Password.ShouldBe(password);
	}

	[Fact]
	public void Constructor_WithNullEmail_ShouldThrowArgumentNullException()
	{
		// Arrange
		string email = null;
		var password = "password";

		// Act & Assert
		Should.Throw<ArgumentNullException>(() => new LoginUserCommand(email, password));
	}

	[Fact]
	public void Constructor_WithNullPassword_ShouldThrowArgumentNullException()
	{
		// Arrange
		var email = "test@example.com";
		string password = null;

		// Act & Assert
		Should.Throw<ArgumentNullException>(() => new LoginUserCommand(email, password));
	}
}