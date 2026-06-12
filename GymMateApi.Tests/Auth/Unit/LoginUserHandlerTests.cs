using GymMateApi.Application.Exceptions;
using GymMateApi.Application.Users.Commands.LoginUser;
using GymMateApi.Core.Entities;
using GymMateApi.Infrastructure.Interfaces.Auth;
using GymMateApi.Persistence.Interfaces;
using Moq;
using Xunit;

namespace GymMateApi.Tests.Auth.Unit;

public class LoginUserHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock = new();
    private readonly Mock<IPasswordHasher> _passwordHasherMock = new();
    private readonly Mock<IJwtProvider>    _jwtProviderMock    = new();

    private LoginUserHandler CreateSut() =>
        new(_userRepositoryMock.Object, _passwordHasherMock.Object, _jwtProviderMock.Object);
    
    private static UserEntity MakeUser(string email = "user@gym.com") => new()
    {
        Id           = Guid.NewGuid(),
        UserName     = "gymuser",
        Email        = email,
        PasswordHash = "hashed",
        Role         = "Admin"
    };

    private static LoginUserCommand ValidCommand(
        string email    = "user@gym.com",
        string password = "P@ssw0rd!") =>
        new(email, password);

    [Fact]
    public async Task Handle_ValidCredentials_ReturnsJwtToken()
    {
        // Arrange
        var user = MakeUser();
        _userRepositoryMock
            .Setup(r => r.GetByEmail(user.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);
        _passwordHasherMock
            .Setup(h => h.Verify("P@ssw0rd!", user.PasswordHash))
            .Returns(true);
        _jwtProviderMock
            .Setup(j => j.GenerateToken(user))
            .Returns("jwt-token");

        // Act
        var result = await CreateSut().Handle(ValidCommand(), CancellationToken.None);

        // Assert
        Assert.Equal("jwt-token", result);
    }

    [Fact]
    public async Task Handle_ValidCredentials_CallsJwtProviderOnce()
    {
        // Arrange
        var user = MakeUser();
        _userRepositoryMock
            .Setup(r => r.GetByEmail(user.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);
        _passwordHasherMock
            .Setup(h => h.Verify(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(true);
        _jwtProviderMock
            .Setup(j => j.GenerateToken(It.IsAny<UserEntity>()))
            .Returns("token");

        // Act
        await CreateSut().Handle(ValidCommand(), CancellationToken.None);

        // Assert
        _jwtProviderMock.Verify(j => j.GenerateToken(user), Times.Once);
    }

    [Theory]
    [InlineData("",    "pass")]
    [InlineData("   ", "pass")]
    public async Task Handle_EmptyEmail_ThrowsBadRequestException(string email, string password)
    {
        var ex = await Assert.ThrowsAsync<BadRequestException>(
            () => CreateSut().Handle(new(email, password), CancellationToken.None));

        Assert.Equal("Email field cannot be empty", ex.Message);
        _jwtProviderMock.Verify(j => j.GenerateToken(It.IsAny<UserEntity>()), Times.Never);
    }

    [Theory]
    [InlineData("user@gym.com", "")]
    [InlineData("user@gym.com", "   ")]
    public async Task Handle_EmptyPassword_ThrowsBadRequestException(string email, string password)
    {
        var ex = await Assert.ThrowsAsync<BadRequestException>(
            () => CreateSut().Handle(new LoginUserCommand(email, password), CancellationToken.None));

        Assert.Equal("Password field cannot be empty", ex.Message);
        _jwtProviderMock.Verify(j => j.GenerateToken(It.IsAny<UserEntity>()), Times.Never);
    }

    [Fact]
    public async Task Handle_UserNotFound_ThrowsUnauthorizedAccessException()
    {
        // Arrange
        _userRepositoryMock
            .Setup(r => r.GetByEmail(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((UserEntity?)null);

        // Act & Assert
        await Assert.ThrowsAsync<UnauthorizedAccessException>(
            () => CreateSut().Handle(ValidCommand(email: "ghost@gym.com"), CancellationToken.None));

        _jwtProviderMock.Verify(j => j.GenerateToken(It.IsAny<UserEntity>()), Times.Never);
    }

    [Fact]
    public async Task Handle_WrongPassword_ThrowsUnauthorizedAccessException()
    {
        // Arrange
        var user = MakeUser();
        _userRepositoryMock
            .Setup(r => r.GetByEmail(user.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);
        _passwordHasherMock
            .Setup(h => h.Verify(It.IsAny<string>(), user.PasswordHash))
            .Returns(false);

        // Act & Assert
        await Assert.ThrowsAsync<UnauthorizedAccessException>(
            () => CreateSut().Handle(ValidCommand(password: "WrongPass!"), CancellationToken.None));

        _jwtProviderMock.Verify(j => j.GenerateToken(It.IsAny<UserEntity>()), Times.Never);
    }

    [Fact]
    public async Task Handle_WrongPassword_SameErrorMessageAsUserNotFound()
    {
        // Security: both cases must return the same message so the caller
        // cannot enumerate whether an email is registered or not.
        var user = MakeUser();

        // Case 1 — wrong password
        _userRepositoryMock
            .Setup(r => r.GetByEmail(user.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);
        _passwordHasherMock
            .Setup(h => h.Verify(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(false);

        var exWrongPass = await Assert.ThrowsAsync<UnauthorizedAccessException>(
            () => CreateSut().Handle(ValidCommand(), CancellationToken.None));

        // Case 2 — user not found
        _userRepositoryMock
            .Setup(r => r.GetByEmail(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((UserEntity?)null);

        var exNotFound = await Assert.ThrowsAsync<UnauthorizedAccessException>(
            () => CreateSut().Handle(ValidCommand(email: "ghost@gym.com"), CancellationToken.None));

        Assert.Equal(exWrongPass.Message, exNotFound.Message);
    }
}