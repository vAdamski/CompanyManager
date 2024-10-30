using System.Security.Claims;
using CompanyManager.Application.Common.Interfaces.Application.Services;
using CompanyManager.Application.Common.Managers;
using CompanyManager.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using Shouldly;

public class JwtTokenManagerTests
{
	private readonly Mock<IConfiguration> _configurationMock;
	private readonly Mock<IJwtTokenGenerator> _jwtTokenGeneratorMock;
	private readonly Mock<IRefreshTokenGenerator> _refreshTokenGeneratorMock;
	private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;

	public JwtTokenManagerTests()
	{
		_configurationMock = new Mock<IConfiguration>();
		_jwtTokenGeneratorMock = new Mock<IJwtTokenGenerator>();
		_refreshTokenGeneratorMock = new Mock<IRefreshTokenGenerator>();
		_userManagerMock = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), null, null,
			null, null, null, null, null, null);
	}

	[Fact]
	public async Task GenerateJwtAndRefreshToken_ShouldReturnTokens_WhenUserIsValid()
	{
		// Arrange
		var email = "test@example.com";
		var user = ApplicationUser.Create(email);
		var tokenResponse = "jwt_token";
		var refreshToken = "refresh_token";
		var refreshTokenExpiration = DateTime.UtcNow.AddDays(7);

		_userManagerMock.Setup(u => u.FindByEmailAsync(email)).ReturnsAsync(user);
		_userManagerMock.Setup(u => u.GetRolesAsync(user)).ReturnsAsync(new List<string>());
		_userManagerMock.Setup(u => u.GetClaimsAsync(user)).ReturnsAsync(new List<Claim>());
		_jwtTokenGeneratorMock.Setup(j => j.GenerateToken(user, It.IsAny<List<string>>(), It.IsAny<List<Claim>>()))
			.ReturnsAsync(tokenResponse);
		_refreshTokenGeneratorMock.Setup(r => r.GenerateRefreshToken()).Returns(refreshToken);
		_refreshTokenGeneratorMock.Setup(r => r.GetRefreshTokenExpiry()).Returns(refreshTokenExpiration);

		var jwtTokenManager = new JwtTokenManager(_configurationMock.Object, _jwtTokenGeneratorMock.Object,
			_refreshTokenGeneratorMock.Object, _userManagerMock.Object);

		// Act
		var result = await jwtTokenManager.GenerateJwtAndRefreshToken(email);

		// Assert
		result.IsSuccess.ShouldBeTrue();
		result.Value.JwtToken.ShouldBe(tokenResponse);
		result.Value.RefreshToken.ShouldBe(refreshToken);

		_userManagerMock.Verify(u => u.UpdateAsync(user), Times.Once);
	}
}