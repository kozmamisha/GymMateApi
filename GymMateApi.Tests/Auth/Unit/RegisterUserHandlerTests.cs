using GymMateApi.Shared.Exceptions;
using GymMateApi.AuthService.Application.Users.Commands.RegisterUser;
using GymMateApi.AuthService.Core;
using GymMateApi.AuthService.Infrastructure.Interfaces.Auth;
using GymMateApi.AuthService.Persistance.Interfaces;
using Moq;
using Xunit;

namespace GymMateApi.Tests.Auth.Unit;

public class RegisterUserHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock = new();
    private readonly Mock<IPasswordHasher> _passwordHasherMock = new();

    private RegisterUserHandler CreateSut() =>
        new(_userRepositoryMock.Object, _passwordHasherMock.Object);
    
    private void SetupNoExistingUser() =>
        _userRepositoryMock
            .Setup(r => r.GetByEmail(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((UserEntity?)null);

    private static RegisterUserCommand ValidCommand(
        string userName = "John",
        string email    = "john@gym.com",
        string password = "P@ssw0rd!") =>
        new(userName, email, password);

    [Fact]
    public async Task Handle_ValidCommand_AddsUserToRepository()
    {
        // Arrange
        SetupNoExistingUser();
        _passwordHasherMock
            .Setup(h => h.Generate("P@ssw0rd!"))
            .Returns("hashed-password");

        // Act
        await CreateSut().Handle(ValidCommand(), CancellationToken.None);

        // Assert
        _userRepositoryMock.Verify(
            r => r.Add(
                It.Is<UserEntity>(u =>
                    u.UserName     == "John"            &&
                    u.Email        == "john@gym.com"    &&
                    u.PasswordHash == "hashed-password"),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_ValidCommand_AssignsNewGuidToUser()
    {
        // Arrange
        SetupNoExistingUser();
        _passwordHasherMock.Setup(h => h.Generate(It.IsAny<string>())).Returns("h");

        UserEntity? captured = null;
        _userRepositoryMock
            .Setup(r => r.Add(It.IsAny<UserEntity>(), It.IsAny<CancellationToken>()))
            .Callback<UserEntity, CancellationToken>((u, _) => captured = u);

        // Act
        await CreateSut().Handle(ValidCommand(), CancellationToken.None);

        // Assert
        Assert.NotNull(captured);
        Assert.NotEqual(Guid.Empty, captured.Id);
    }

    [Fact]
    public async Task Handle_ValidCommand_HashesPasswordBeforeSaving()
    {
        // Arrange
        SetupNoExistingUser();
        _passwordHasherMock
            .Setup(h => h.Generate("P@ssw0rd!"))
            .Returns("bcrypt-hash");

        UserEntity? captured = null;
        _userRepositoryMock
            .Setup(r => r.Add(It.IsAny<UserEntity>(), It.IsAny<CancellationToken>()))
            .Callback<UserEntity, CancellationToken>((u, _) => captured = u);

        // Act
        await CreateSut().Handle(ValidCommand(), CancellationToken.None);

        // Assert
        Assert.NotNull(captured);
        Assert.NotEqual("P@ssw0rd!", captured.PasswordHash);
        Assert.Equal("bcrypt-hash", captured.PasswordHash);
    }

    [Theory]
    [InlineData("",    "john@gym.com", "pass")]
    [InlineData("   ", "john@gym.com", "pass")]
    public async Task Handle_EmptyUserName_ThrowsBadRequestException(
        string userName, string email, string password)
    {
        var ex = await Assert.ThrowsAsync<BadRequestException>(
            () => CreateSut().Handle(new(userName, email, password), CancellationToken.None));

        Assert.Equal("User name cannot be empty", ex.Message);
        _userRepositoryMock.Verify(r => r.Add(It.IsAny<UserEntity>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Theory]
    [InlineData("John", "",    "pass")]
    [InlineData("John", "   ", "pass")]
    public async Task Handle_EmptyEmail_ThrowsBadRequestException(
        string userName, string email, string password)
    {
        var ex = await Assert.ThrowsAsync<BadRequestException>(
            () => CreateSut().Handle(new(userName, email, password), CancellationToken.None));

        Assert.Equal("Email field cannot be empty", ex.Message);
        _userRepositoryMock.Verify(r => r.Add(It.IsAny<UserEntity>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Theory]
    [InlineData("John", "john@gym.com", "")]
    [InlineData("John", "john@gym.com", "   ")]
    public async Task Handle_EmptyPassword_ThrowsBadRequestException(
        string userName, string email, string password)
    {
        var ex = await Assert.ThrowsAsync<BadRequestException>(
            () => CreateSut().Handle(new(userName, email, password), CancellationToken.None));

        Assert.Equal("Password field cannot be empty", ex.Message);
        _userRepositoryMock.Verify(r => r.Add(It.IsAny<UserEntity>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_DuplicateEmail_ThrowsBadRequestException()
    {
        // Arrange
        var existing = new UserEntity { Id = Guid.NewGuid(), Email = "dup@gym.com" };
        _userRepositoryMock
            .Setup(r => r.GetByEmail("dup@gym.com", It.IsAny<CancellationToken>()))
            .ReturnsAsync(existing);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<BadRequestException>(
            () => CreateSut().Handle(ValidCommand(email: "dup@gym.com"), CancellationToken.None));

        Assert.Equal("User with this email already exists", ex.Message);
        _userRepositoryMock.Verify(r => r.Add(It.IsAny<UserEntity>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_DuplicateEmail_DoesNotCallPasswordHasher()
    {
        // Arrange
        var existing = new UserEntity { Id = Guid.NewGuid(), Email = "dup@gym.com" };
        _userRepositoryMock
            .Setup(r => r.GetByEmail("dup@gym.com", It.IsAny<CancellationToken>()))
            .ReturnsAsync(existing);

        // Act
        await Assert.ThrowsAsync<BadRequestException>(
            () => CreateSut().Handle(ValidCommand(email: "dup@gym.com"), CancellationToken.None));

        // Assert
        _passwordHasherMock.Verify(h => h.Generate(It.IsAny<string>()), Times.Never);
    }
}