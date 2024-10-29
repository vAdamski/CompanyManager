using System.Security.Claims;
using CompanyManager.Application.Actions.AuthActions.Commands.LoginUser;
using CompanyManager.Application.Common.Interfaces.Application.Services;
using CompanyManager.Domain.Entities;
using CompanyManager.Domain.Errors;
using Microsoft.AspNetCore.Identity;
using Moq;
using Shouldly;

namespace CompanyManager.UnitTests.Application.Actions.AuthActions.Commands.LoginUser;

public class LoginUserCommandHandlerTests
{
	private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
	private readonly Mock<IJwtTokenGenerator> _jwtTokenGeneratorMock;
	private readonly Mock<IRefreshTokenGenerator> _refreshTokenGeneratorMock;
	private readonly LoginUserCommandHandler _handler;

	public LoginUserCommandHandlerTests()
	{
		var store = new Mock<IUserStore<ApplicationUser>>();
		_userManagerMock =
			new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);
		_jwtTokenGeneratorMock = new Mock<IJwtTokenGenerator>();
		_refreshTokenGeneratorMock = new Mock<IRefreshTokenGenerator>();
		_handler = new LoginUserCommandHandler(_userManagerMock.Object, _jwtTokenGeneratorMock.Object, _refreshTokenGeneratorMock.Object);
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
	public async Task Handle_ValidCredentials_ReturnsSuccessWithToken()
	{
		// Arrange
		var user = ApplicationUser.Create("user@example.com");
		var command = new LoginUserCommand("user@example.com", "correctpassword");
		var roles = new List<string> { "Role1", "Role2" };
		var claims = new List<Claim> { new Claim("type", "value") };
		var token = "token";

		_userManagerMock.Setup(x => x.FindByEmailAsync(command.Email)).ReturnsAsync(user);
		_userManagerMock.Setup(x => x.CheckPasswordAsync(user, command.Password)).ReturnsAsync(true);
		_userManagerMock.Setup(x => x.GetRolesAsync(user)).ReturnsAsync(roles);
		_userManagerMock.Setup(x => x.GetClaimsAsync(user)).ReturnsAsync(claims);
		_jwtTokenGeneratorMock.Setup(x => x.GenerateToken(user, It.IsAny<List<string>>(), It.IsAny<List<Claim>>()))
			.ReturnsAsync(token);
		_refreshTokenGeneratorMock.Setup(x => x.GenerateRefreshToken()).Returns("refresh");

		// Act
		var result = await _handler.Handle(command, CancellationToken.None);

		// Assert
		result.ShouldNotBeNull();
		result.IsSuccess.ShouldBeTrue();
		result.Value.JwtToken.ShouldBe(token);
		result.Value.RefreshToken.ShouldNotBeNull();
	}
}