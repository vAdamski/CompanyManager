using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CompanyManager.Application.Common.Services;
using CompanyManager.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Moq;
using Shouldly;

namespace CompanyManager.UnitTests.Application.Services;

public class JwtTokenGeneratorTests
{
	private readonly JwtTokenGenerator _jwtTokenGenerator;
	private readonly Mock<IConfiguration> _mockConfiguration;
	
	private readonly Guid _secret = Guid.NewGuid();

	public JwtTokenGeneratorTests()
	{
		_mockConfiguration = new Mock<IConfiguration>();

		_mockConfiguration.Setup(config => config["Jwt:Secret"]).Returns(_secret.ToString());
		_mockConfiguration.Setup(config => config["Jwt:ExpirationTime"]).Returns("600");

		_jwtTokenGenerator = new JwtTokenGenerator(_mockConfiguration.Object);
	}

	[Fact]
	public async Task GenerateToken_Should_Return_Token()
	{
		// Arrange
		var user = ApplicationUser.Create("test@example.com");

		// Act
		var token = await _jwtTokenGenerator.GenerateToken(user);

		// Assert
		token.ShouldNotBeNull();
		token.ShouldNotBe(string.Empty);
	}

	[Fact]
	public async Task GenerateToken_Should_Include_Roles_And_Claims()
	{
		// Arrange
		var user = ApplicationUser.Create("test@example.com");

		var roles = new List<string> { "Admin", "User" };
		var extraClaims = new List<Claim>
		{
			new Claim("CustomClaimType", "CustomClaimValue")
		};

		// Act
		var token = await _jwtTokenGenerator.GenerateToken(user, roles, extraClaims);

		// Assert
		
		var claimsInToken = new JwtSecurityTokenHandler().ReadJwtToken(token).Claims.ToList();
		
		token.ShouldNotBeNull();
		claimsInToken.ShouldContain(claim => claim.Type == "email" && claim.Value == user.Email);
		claimsInToken.ShouldContain(claim => claim.Type == "role" && claim.Value == "Admin");
		claimsInToken.ShouldContain(claim => claim.Type == "role" && claim.Value == "User");
		claimsInToken.ShouldContain(claim => claim.Type == "CustomClaimType" && claim.Value == "CustomClaimValue");
	}

	[Fact]
	public void JwtTokenGenerator_Should_Initialize_Correctly()
	{
		// Arrange & Act
		var expirationTime = int.Parse(_mockConfiguration.Object["Jwt:ExpirationTime"]);
		var secret = _mockConfiguration.Object["Jwt:Secret"];

		// Assert
		_jwtTokenGenerator.ShouldNotBeNull();
		expirationTime.ShouldBe(600);
		secret.ShouldBe(_secret.ToString());
	}
}