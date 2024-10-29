using CompanyManager.Application.Actions.AuthActions.Commands.RegisterUser;
using Shouldly;

namespace CompanyManager.UnitTests.Application.Actions.AuthActions.Commands.RegisterUser;

public class RegisterUserCommandTests
{
	[Fact]
	public void RegisterUserCommand_ShouldSetPropertiesCorrectly()
	{
		// Arrange
		var email = "test@example.com";
		var password = "Password123!";
		var role = "Admin";

		// Act
		var command = new RegisterUserCommand(email, password, role);

		// Assert
		command.Email.ShouldBe(email);
		command.Password.ShouldBe(password);
		command.Role.ShouldBe(role);
	}

	[Fact]
	public void RegisterUserCommand_ShouldSetDefaultRoleToUser()
	{
		// Arrange
		var email = "test@example.com";
		var password = "Password123!";

		// Act
		var command = new RegisterUserCommand(email, password);

		// Assert
		command.Email.ShouldBe(email);
		command.Password.ShouldBe(password);
		command.Role.ShouldBe("User");
	}

	[Theory]
	[InlineData(null)]
	[InlineData("")]
	[InlineData(" ")]
	public void RegisterUserCommand_ShouldThrowException_WhenEmailIsInvalid(string email)
	{
		// Arrange
		var password = "Password123!";

		// Act & Assert
		Should.Throw<ArgumentException>(() => new RegisterUserCommand(email, password));
	}

	[Theory]
	[InlineData(null)]
	[InlineData("")]
	[InlineData(" ")]
	public void RegisterUserCommand_ShouldThrowException_WhenPasswordIsInvalid(string password)
	{
		// Arrange
		var email = "test@example.com";

		// Act & Assert
		Should.Throw<ArgumentException>(() => new RegisterUserCommand(email, password));
	}
}