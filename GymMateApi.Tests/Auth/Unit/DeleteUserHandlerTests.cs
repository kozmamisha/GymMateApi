using GymMateApi.Shared.Exceptions;
using GymMateApi.AuthService.Application.Users.Commands.DeleteUser;
using GymMateApi.AuthService.Core;
using GymMateApi.AuthService.Persistance.Interfaces;
using Moq;
using Xunit;

namespace GymMateApi.Tests.Auth.Unit;

public class DeleteUserHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock = new();

    private DeleteUserHandler CreateSut() => new(_userRepositoryMock.Object);
    
    private static UserEntity MakeUser() => new()
    {
        Id           = Guid.NewGuid(),
        UserName     = "gymuser",
        Email        = "user@gym.com",
        PasswordHash = "hashed"
    };

    [Fact]
    public async Task Handle_OwnAccount_DeletesUser()
    {
        // Arrange
        var user = MakeUser();
        _userRepositoryMock
            .Setup(r => r.GetUserById(user.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        var command = new DeleteUserCommand(Id: user.Id, CurrentUserId: user.Id);

        // Act
        await CreateSut().Handle(command, CancellationToken.None);

        // Assert
        _userRepositoryMock.Verify(
            r => r.Delete(user, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_DifferentCurrentUserId_ThrowsBadRequestException()
    {
        // Arrange
        var targetId    = Guid.NewGuid();
        var requesterId = Guid.NewGuid(); // different user

        var command = new DeleteUserCommand(Id: targetId, CurrentUserId: requesterId);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<BadRequestException>(
            () => CreateSut().Handle(command, CancellationToken.None));

        Assert.Equal("You can not delete other user", ex.Message);
    }

    [Fact]
    public async Task Handle_DifferentCurrentUserId_NeverCallsRepository()
    {
        // Arrange
        var command = new DeleteUserCommand(Id: Guid.NewGuid(), CurrentUserId: Guid.NewGuid());

        // Act
        await Assert.ThrowsAsync<BadRequestException>(
            () => CreateSut().Handle(command, CancellationToken.None));

        // Assert
        _userRepositoryMock.Verify(
            r => r.GetUserById(It.IsAny<Guid>(), It.IsAny<CancellationToken>()),
            Times.Never);
        _userRepositoryMock.Verify(
            r => r.Delete(It.IsAny<UserEntity>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task Handle_UserDoesNotExist_ThrowsEntityNotFoundException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _userRepositoryMock
            .Setup(r => r.GetUserById(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((UserEntity?)null);

        var command = new DeleteUserCommand(Id: userId, CurrentUserId: userId);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<EntityNotFoundException>(
            () => CreateSut().Handle(command, CancellationToken.None));

        Assert.Equal("User not found.", ex.Message);
    }

    [Fact]
    public async Task Handle_UserDoesNotExist_NeverCallsDelete()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _userRepositoryMock
            .Setup(r => r.GetUserById(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((UserEntity?)null);

        var command = new DeleteUserCommand(Id: userId, CurrentUserId: userId);

        // Act
        await Assert.ThrowsAsync<EntityNotFoundException>(
            () => CreateSut().Handle(command, CancellationToken.None));

        // Assert
        _userRepositoryMock.Verify(
            r => r.Delete(It.IsAny<UserEntity>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }
}