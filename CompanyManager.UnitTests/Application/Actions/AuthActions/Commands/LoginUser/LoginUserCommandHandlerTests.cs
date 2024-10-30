using CompanyManager.Application.Actions.AuthActions.Commands.LoginUser;
using CompanyManager.Application.Common.Interfaces.Application.Managers;
using CompanyManager.Application.Common.Models;
using CompanyManager.Domain.Common;
using CompanyManager.Domain.Entities;
using CompanyManager.Domain.Errors;
using Microsoft.AspNetCore.Identity;
using Moq;
using Shouldly;

namespace CompanyManager.UnitTests.Application.Actions.AuthActions.Commands.LoginUser;

public class LoginUserCommandHandlerTests
{
	private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
	private readonly Mock<IJwtTokenManager> _jwtTokenManagerMock;
	private readonly LoginUserCommandHandler _handler;

	public LoginUserCommandHandlerTests()
	{
		var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
		_userManagerMock = new Mock<UserManager<ApplicationUser>>(userStoreMock.Object, null, null, null, null,
			null, null, null, null);
		_jwtTokenManagerMock = new Mock<IJwtTokenManager>();
		_handler = new LoginUserCommandHandler(_userManagerMock.Object, _jwtTokenManagerMock.Object);
	}

	[Fact]
	public async Task Handle_UserNotFound_ReturnsFailure()
	{
		// Arrange
		var command = new LoginUserCommand("nonexistent@example.com", "password");
		_userManagerMock.Setup(x => x.FindByEmailAsync(command.Email)).ReturnsAsync((ApplicationUser)null);

		// Act
		var result = await _handler.Handle(command, CancellationToken.None);

		// Assert
		result.ShouldNotBeNull();
		result.IsSuccess.ShouldBeFalse();
		result.Error.ShouldBe(DomainErrors.User.UserNotFound);
	}

	[Fact]
	public async Task Handle_InvalidPassword_ReturnsFailure()
	{
		// Arrange
		var user = ApplicationUser.Create("user@example.com");
		var command = new LoginUserCommand("user@example.com", "wrongpassword");
		_userManagerMock.Setup(x => x.FindByEmailAsync(command.Email)).ReturnsAsync(user);
		_userManagerMock.Setup(x => x.CheckPasswordAsync(user, command.Password)).ReturnsAsync(false);

		// Act
		var result = await _handler.Handle(command, CancellationToken.None);

		// Assert
		result.ShouldNotBeNull();
		result.IsSuccess.ShouldBeFalse();
		result.Error.ShouldBe(DomainErrors.User.InvalidPassword);
	}

	[Fact]
	public async Task Handle_ValidCredentials_ReturnsSuccess()
	{
		// Arrange
		var user = ApplicationUser.Create("user@example.com");
		var command = new LoginUserCommand("user@example.com", "correctpassword");
		_userManagerMock.Setup(x => x.FindByEmailAsync(command.Email)).ReturnsAsync(user);
		_userManagerMock.Setup(x => x.CheckPasswordAsync(user, command.Password)).ReturnsAsync(true);
		var tokenResponse = JwtTokenResponse.Success("jwt_token", "refresh_token");
		_jwtTokenManagerMock.Setup(x => x.GenerateJwtAndRefreshToken(command.Email))
			.ReturnsAsync(Result.Success(tokenResponse));

		// Act
		var result = await _handler.Handle(command, CancellationToken.None);

		// Assert
		result.ShouldNotBeNull();
		result.IsSuccess.ShouldBeTrue();
		result.Value.ShouldBe(tokenResponse);
	}
}