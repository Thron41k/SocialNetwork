using Moq;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories.Interfaces;
using static NUnit.Framework.Assert;

namespace SocialNetwork.Tests.BLL.Services;

[TestFixture]
public class UserServiceTests
{
    private Mock<IUserRepository> _userRepositoryMock;
    private Mock<IFriendRepository> _friendRepositoryMock;
    private UserService _userService;

    [SetUp]
    public void SetUp()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _friendRepositoryMock = new Mock<IFriendRepository>();
        _userService = new UserService(_userRepositoryMock.Object, _friendRepositoryMock.Object);
    }

    [Test]
    public void Register_ThrowsArgumentNullException_WhenFirstNameIsNullOrEmpty()
    {
        var userRegistrationData = new UserRegistrationData
        {
            FirstName = "",
            LastName = "Strange",
            Password = "password123",
            Email = "man.strange@example.com"
        };
        Throws<ArgumentNullException>(() => _userService.Register(userRegistrationData));
    }

    [Test]
    public void Register_ThrowsArgumentNullException_WhenEmailIsInvalid()
    {
        var userRegistrationData = new UserRegistrationData
        {
            FirstName = "Man",
            LastName = "Strange",
            Password = "password123",
            Email = "invalid-email"
        };
        Throws<ArgumentNullException>(() => _userService.Register(userRegistrationData));
    }

    [Test]
    public void Register_ThrowsArgumentNullException_WhenEmailAlreadyExists()
    {
        var userRegistrationData = new UserRegistrationData
        {
            FirstName = "Man",
            LastName = "Strange",
            Password = "password123",
            Email = "man.strange@example.com"
        };
        _userRepositoryMock.Setup(repo => repo.FindByEmail(userRegistrationData.Email)).Returns(new UserEntity());
        Throws<ArgumentNullException>(() => _userService.Register(userRegistrationData));
    }

    [Test]
    public void Register_SuccessfullyCreatesUser()
    {
        var userRegistrationData = new UserRegistrationData
        {
            FirstName = "Man",
            LastName = "Strange",
            Password = "password123",
            Email = "man.strange@example.com"
        };
        _userRepositoryMock.Setup(repo => repo.FindByEmail(userRegistrationData.Email)).Returns((UserEntity)null!);
        _userRepositoryMock.Setup(repo => repo.Create(It.IsAny<UserEntity>())).Returns(1);
        _userService.Register(userRegistrationData);
        _userRepositoryMock.Verify(repo => repo.Create(It.IsAny<UserEntity>()), Times.Once);
    }

    [Test]
    public void Authenticate_ThrowsUserNotFoundException_WhenUserNotFound()
    {
        var userAuthenticationData = new UserAuthenticationData
        {
            Email = "man.strange@example.com",
            Password = "password123"
        };
        _userRepositoryMock.Setup(repo => repo.FindByEmail(userAuthenticationData.Email)).Returns((UserEntity)null!);
        Throws<UserNotFoundException>(() => _userService.Authenticate(userAuthenticationData));
    }

    [Test]
    public void Authenticate_ThrowsWrongPasswordException_WhenPasswordIsIncorrect()
    {
        var userAuthenticationData = new UserAuthenticationData
        {
            Email = "man.strange@example.com",
            Password = "wrongpassword"
        };
        var userEntity = new UserEntity { Email = "man.strange@example.com", Password = "password123" };
        _userRepositoryMock.Setup(repo => repo.FindByEmail(userAuthenticationData.Email)).Returns(userEntity);
        Throws<WrongPasswordException>(() => _userService.Authenticate(userAuthenticationData));
    }

    [Test]
    public void Authenticate_ReturnsUser_WhenCredentialsAreCorrect()
    {
        var userAuthenticationData = new UserAuthenticationData
        {
            Email = "man.strange@example.com",
            Password = "password123"
        };
        var userEntity = new UserEntity
        {
            Id = 1,
            Firstname = "Man",
            Lastname = "Strange",
            Password = "password123",
            Email = "man.strange@example.com"
        };
        _userRepositoryMock.Setup(repo => repo.FindByEmail(userAuthenticationData.Email)).Returns(userEntity);
        var result = _userService.Authenticate(userAuthenticationData);
        That(result.FirstName, Is.EqualTo("Man"));
        That(result.LastName, Is.EqualTo("Strange"));
        That(result.Email, Is.EqualTo("man.strange@example.com"));
    }

    [Test]
    public void FindByEmail_ThrowsUserNotFoundException_WhenUserNotFound()
    {
        var email = "nonexistent@example.com";
        _userRepositoryMock.Setup(repo => repo.FindByEmail(email)).Returns((UserEntity)null!);
        Throws<UserNotFoundException>(() => _userService.FindByEmail(email));
    }

    [Test]
    public void FindByEmail_ReturnsUser_WhenUserExists()
    {
        const string email = "man.strange@example.com";
        var userEntity = new UserEntity
        {
            Id = 1,
            Firstname = "Man",
            Lastname = "Strange",
            Email = email
        };
        _userRepositoryMock.Setup(repo => repo.FindByEmail(email)).Returns(userEntity);
        var result = _userService.FindByEmail(email);
        That(result.FirstName, Is.EqualTo("Man"));
        That(result.LastName, Is.EqualTo("Strange"));
        That(result.Email, Is.EqualTo(email));
    }

    [Test]
    public void AddFriend_ThrowsUserNotFoundException_WhenFriendNotFound()
    {
        var userAddingFriendData = new UserAddingFriendData
        {
            UserId = 1,
            FriendEmail = "nonexistent@example.com"
        };
        _userRepositoryMock.Setup(repo => repo.FindByEmail(userAddingFriendData.FriendEmail)).Returns((UserEntity)null!);
        Throws<UserNotFoundException>(() => _userService.AddFriend(userAddingFriendData));
    }

    [Test]
    public void AddFriend_SuccessfullyAddsFriend()
    {
        var userAddingFriendData = new UserAddingFriendData
        {
            UserId = 1,
            FriendEmail = "friend@example.com"
        };
        var friendEntity = new UserEntity { Id = 2, Email = "friend@example.com" };
        _userRepositoryMock.Setup(repo => repo.FindByEmail(userAddingFriendData.FriendEmail)).Returns(friendEntity);
        _friendRepositoryMock.Setup(repo => repo.Create(It.IsAny<FriendEntity>())).Returns(1);
        _userService.AddFriend(userAddingFriendData);
        _friendRepositoryMock.Verify(repo => repo.Create(It.IsAny<FriendEntity>()), Times.Once);
    }
}