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
    // Моки репозиториев пользователей и друзей, которые будут использоваться в тестах
    private Mock<IUserRepository> _userRepositoryMock;
    private Mock<IFriendRepository> _friendRepositoryMock;

    // Экземпляр UserService для тестирования
    private UserService _userService;

    // Метод, который будет выполнен перед каждым тестом для настройки зависимостей
    [SetUp]
    public void SetUp()
    {
        // Инициализация моков для репозиториев
        _userRepositoryMock = new Mock<IUserRepository>();
        _friendRepositoryMock = new Mock<IFriendRepository>();

        // Инициализация UserService с подмененными зависимостями
        _userService = new UserService(_userRepositoryMock.Object, _friendRepositoryMock.Object);
    }

    // Тест на регистрацию, проверка выбрасывания исключения ArgumentNullException, если FirstName пустой или равен null
    [Test]
    public void Register_ThrowsArgumentNullException_WhenFirstNameIsNullOrEmpty()
    {
        var userRegistrationData = new UserRegistrationData
        {
            FirstName = "",  // Пустое имя
            LastName = "Strange",
            Password = "password123",
            Email = "man.strange@example.com"
        };

        // Проверка, что при регистрации выбрасывается ArgumentNullException
        Throws<ArgumentNullException>(() => _userService.Register(userRegistrationData));
    }

    // Тест на регистрацию, проверка выбрасывания исключения ArgumentException, если email некорректный
    [Test]
    public void Register_ThrowsArgumentException_WhenEmailIsInvalid()
    {
        var userRegistrationData = new UserRegistrationData
        {
            FirstName = "Man",
            LastName = "Strange",
            Password = "password123",
            Email = "invalid-email"  // Некорректный email
        };

        // Проверка, что при регистрации выбрасывается ArgumentException
        Throws<ArgumentException>(() => _userService.Register(userRegistrationData));
    }

    // Тест на регистрацию, проверка выбрасывания исключения ArgumentException, если email уже существует
    [Test]
    public void Register_ThrowsArgumentException_WhenEmailAlreadyExists()
    {
        var userRegistrationData = new UserRegistrationData
        {
            FirstName = "Man",
            LastName = "Strange",
            Password = "password123",
            Email = "man.strange@example.com"
        };

        // Мок репозитория для проверки существования email
        _userRepositoryMock.Setup(repo => repo.FindByEmail(userRegistrationData.Email)).Returns(new UserEntity());

        // Проверка, что при регистрации выбрасывается ArgumentException, если email уже существует
        Throws<ArgumentException>(() => _userService.Register(userRegistrationData));
    }

    // Тест на регистрацию, проверка успешного создания пользователя
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

        // Мок репозитория для проверки отсутствия пользователя с таким email
        _userRepositoryMock.Setup(repo => repo.FindByEmail(userRegistrationData.Email)).Returns((UserEntity)null!);

        // Мок репозитория для создания пользователя
        _userRepositoryMock.Setup(repo => repo.Create(It.IsAny<UserEntity>())).Returns(1);

        // Выполнение регистрации
        _userService.Register(userRegistrationData);

        // Проверка, что метод Create был вызван один раз
        _userRepositoryMock.Verify(repo => repo.Create(It.IsAny<UserEntity>()), Times.Once);
    }

    // Тест на аутентификацию, проверка выбрасывания исключения UserNotFoundException, если пользователь не найден
    [Test]
    public void Authenticate_ThrowsUserNotFoundException_WhenUserNotFound()
    {
        var userAuthenticationData = new UserAuthenticationData
        {
            Email = "man.strange@example.com",
            Password = "password123"
        };

        // Мок репозитория для возвращения null (пользователь не найден)
        _userRepositoryMock.Setup(repo => repo.FindByEmail(userAuthenticationData.Email)).Returns((UserEntity)null!);

        // Проверка, что выбрасывается UserNotFoundException
        Throws<UserNotFoundException>(() => _userService.Authenticate(userAuthenticationData));
    }

    // Тест на аутентификацию, проверка выбрасывания исключения WrongPasswordException, если пароль неверный
    [Test]
    public void Authenticate_ThrowsWrongPasswordException_WhenPasswordIsIncorrect()
    {
        var userAuthenticationData = new UserAuthenticationData
        {
            Email = "man.strange@example.com",
            Password = "wrongpassword"  // Неверный пароль
        };
        var userEntity = new UserEntity { Email = "man.strange@example.com", Password = "password123" };

        // Мок репозитория для возвращения пользователя с неверным паролем
        _userRepositoryMock.Setup(repo => repo.FindByEmail(userAuthenticationData.Email)).Returns(userEntity);

        // Проверка, что выбрасывается WrongPasswordException
        Throws<WrongPasswordException>(() => _userService.Authenticate(userAuthenticationData));
    }

    // Тест на аутентификацию, проверка успешной аутентификации при правильных данных
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

        // Мок репозитория для возвращения существующего пользователя
        _userRepositoryMock.Setup(repo => repo.FindByEmail(userAuthenticationData.Email)).Returns(userEntity);

        // Выполнение аутентификации
        var result = _userService.Authenticate(userAuthenticationData);

        // Проверка, что данные пользователя соответствуют ожиданиям
        That(result.FirstName, Is.EqualTo("Man"));
        That(result.LastName, Is.EqualTo("Strange"));
        That(result.Email, Is.EqualTo("man.strange@example.com"));
    }

    // Тест на поиск пользователя по email, проверка выбрасывания исключения UserNotFoundException, если пользователь не найден
    [Test]
    public void FindByEmail_ThrowsUserNotFoundException_WhenUserNotFound()
    {
        var email = "nonexistent@example.com";

        // Мок репозитория для возвращения null (пользователь не найден)
        _userRepositoryMock.Setup(repo => repo.FindByEmail(email)).Returns((UserEntity)null!);

        // Проверка, что выбрасывается UserNotFoundException
        Throws<UserNotFoundException>(() => _userService.FindByEmail(email));
    }

    // Тест на поиск пользователя по email, проверка успешного возвращения пользователя
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

        // Мок репозитория для возвращения существующего пользователя
        _userRepositoryMock.Setup(repo => repo.FindByEmail(email)).Returns(userEntity);

        // Выполнение поиска
        var result = _userService.FindByEmail(email);

        // Проверка, что данные пользователя соответствуют ожиданиям
        That(result.FirstName, Is.EqualTo("Man"));
        That(result.LastName, Is.EqualTo("Strange"));
        That(result.Email, Is.EqualTo(email));
    }

    // Тест на добавление друга, проверка выбрасывания исключения UserNotFoundException, если друг не найден
    [Test]
    public void AddFriend_ThrowsUserNotFoundException_WhenFriendNotFound()
    {
        var userAddingFriendData = new UserAddingFriendData
        {
            UserId = 1,
            FriendEmail = "nonexistent@example.com"
        };

        // Мок репозитория для возвращения null (друг не найден)
        _userRepositoryMock.Setup(repo => repo.FindByEmail(userAddingFriendData.FriendEmail)).Returns((UserEntity)null!);

        // Проверка, что выбрасывается UserNotFoundException
        Throws<UserNotFoundException>(() => _userService.AddFriend(userAddingFriendData));
    }

    // Тест на добавление друга, проверка успешного добавления друга
    [Test]
    public void AddFriend_SuccessfullyAddsFriend()
    {
        var userAddingFriendData = new UserAddingFriendData
        {
            UserId = 1,
            FriendEmail = "friend@example.com"
        };
        var friendEntity = new UserEntity { Id = 2, Email = "friend@example.com" };

        // Мок репозитория для поиска друга по email
        _userRepositoryMock.Setup(repo => repo.FindByEmail(userAddingFriendData.FriendEmail)).Returns(friendEntity);

        // Мок репозитория для создания записи о дружбе
        _friendRepositoryMock.Setup(repo => repo.Create(It.IsAny<FriendEntity>())).Returns(1);

        // Выполнение добавления друга
        _userService.AddFriend(userAddingFriendData);

        // Проверка, что метод Create был вызван один раз
        _friendRepositoryMock.Verify(repo => repo.Create(It.IsAny<FriendEntity>()), Times.Once);
    }
}
