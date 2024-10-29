using CompanyManager.Application.Common.Interfaces.Application.Services;
using CompanyManager.Application.Common.Interfaces.Infrastructure.Services;
using CompanyManager.Application.Common.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using Shouldly;

namespace CompanyManager.UnitTests.Application.Services;

public class RefreshTokenGeneratorTests
{
	private readonly Mock<IConfiguration> _configurationMock;
	private readonly Mock<IDateTime> _dateTimeMock;
	private readonly IRefreshTokenGenerator _refreshTokenGenerator;

	public RefreshTokenGeneratorTests()
	{
		_configurationMock = new Mock<IConfiguration>();
		_configurationMock.Setup(config => config["Jwt:RefreshTokenExpiration"]).Returns("3600");

		_dateTimeMock = new Mock<IDateTime>();
		_dateTimeMock.Setup(dateTime => dateTime.Now).Returns(new DateTime(2023, 11, 15));

		_refreshTokenGenerator = new RefreshTokenGenerator(_configurationMock.Object, _dateTimeMock.Object);
	}

	[Fact]
	public void GenerateRefreshToken_ShouldReturnBase64String()
	{
		var refreshToken = _refreshTokenGenerator.GenerateRefreshToken();

		// Shouldly assertion to check if the refresh token is a valid Base64 string
		Should.NotThrow(() => Convert.FromBase64String(refreshToken));
		refreshToken.Length.ShouldBeGreaterThan(0);
	}

	[Fact]
	public void GetRefreshTokenExpiry_ShouldReturnCorrectExpiryDate()
	{
		var expectedExpiryDate = new DateTime(2023, 11, 15).AddSeconds(3600);
		var actualExpiryDate = _refreshTokenGenerator.GetRefreshTokenExpiry();

		actualExpiryDate.ShouldBe(expectedExpiryDate);
	}
}