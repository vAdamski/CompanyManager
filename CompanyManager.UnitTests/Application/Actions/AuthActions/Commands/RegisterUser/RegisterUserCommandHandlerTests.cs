using CompanyManager.Application.Actions.AuthActions.Commands.RegisterUser;
using CompanyManager.Domain.Entities;
using CompanyManager.Domain.Errors;
using Microsoft.AspNetCore.Identity;
using Moq;
using Shouldly;

namespace CompanyManager.UnitTests.Application.Actions.AuthActions.Commands.RegisterUser;

public class RegisterUserCommandHandlerTests
{
    private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
    private readonly Mock<RoleManager<IdentityRole>> _roleManagerMock;
    private readonly RegisterUserCommandHandler _handler;

    public RegisterUserCommandHandlerTests()
    {
        // Setup mocks
        _userManagerMock = new Mock<UserManager<ApplicationUser>>(
            Mock.Of<IUserStore<ApplicationUser>>(),
            null, null, null, null, null, null, null, null);

        _roleManagerMock = new Mock<RoleManager<IdentityRole>>(
            Mock.Of<IRoleStore<IdentityRole>>(),
            null, null, null, null);

        _handler = new RegisterUserCommandHandler(_userManagerMock.Object, _roleManagerMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldFail_WhenRoleIsInvalid()
    {
        // Arrange
        var command = new RegisterUserCommand("test@example.com", "Password123!", "InvalidRole");

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldBe(DomainErrors.Role.NotFound);
    }

    [Fact]
    public async Task Handle_ShouldFail_WhenEmailIsAlreadyInUse()
    {
        // Arrange
        var command = new RegisterUserCommand("test@example.com", "Password123!", "User");
        _userManagerMock.Setup(um => um.FindByEmailAsync(command.Email)).ReturnsAsync(ApplicationUser.Create("test@example.com"));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldBe(DomainErrors.User.EmailAlreadyInUse);
    }

    [Fact]
    public async Task Handle_ShouldFail_WhenUserCreationFails()
    {
        // Arrange
        var command = new RegisterUserCommand("test@example.com", "Password123!", "User");
        _userManagerMock.Setup(um => um.FindByEmailAsync(command.Email)).ReturnsAsync((ApplicationUser)null);
        _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<ApplicationUser>(), command.Password))
            .ReturnsAsync(IdentityResult.Failed());

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldBe(DomainErrors.User.RegisterFailed);
    }

    [Fact]
    public async Task Handle_ShouldFail_WhenAssignRoleFails()
    {
        // Arrange
        var command = new RegisterUserCommand("test@example.com", "Password123!", "User");
        var user = ApplicationUser.Create(command.Email);
        
        _userManagerMock.Setup(um => um.FindByEmailAsync(command.Email)).ReturnsAsync((ApplicationUser)null);
        _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<ApplicationUser>(), command.Password))
            .ReturnsAsync(IdentityResult.Success);
        _roleManagerMock.Setup(rm => rm.FindByNameAsync(command.Role))
            .ReturnsAsync(new IdentityRole(command.Role));
        _userManagerMock.Setup(um => um.AddToRoleAsync(It.IsAny<ApplicationUser>(), command.Role))
            .ReturnsAsync(IdentityResult.Failed());

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldBe(DomainErrors.User.RegisterFailed);
    }

    [Fact]
    public async Task Handle_ShouldSucceed_WhenAllStepsAreSuccessful()
    {
        // Arrange
        var command = new RegisterUserCommand("test@example.com", "Password123!", "User");
        var user = ApplicationUser.Create(command.Email);
        if (user == null) throw new InvalidOperationException("User creation failed during test setup.");

        _userManagerMock.Setup(um => um.FindByEmailAsync(command.Email)).ReturnsAsync((ApplicationUser)null);
        _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
        _roleManagerMock.Setup(rm => rm.FindByNameAsync(command.Role)).ReturnsAsync(new IdentityRole(command.Role));
        _userManagerMock.Setup(um => um.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.ShouldBeTrue();
    }
}