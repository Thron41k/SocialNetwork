using System.ComponentModel.DataAnnotations;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories;
using SocialNetwork.DAL.Repositories.Interfaces;

namespace SocialNetwork.BLL.Services;

/// <summary>
/// Сервис для управления пользователями и их друзьями.
/// </summary>
public class UserService(IUserRepository userRepository, IFriendRepository friendRepository)
{
    private readonly MessageService _messageService = new();

    /// <summary>
    /// Конструктор по умолчанию, использующий стандартные реализации репозиториев.
    /// </summary>
    public UserService() : this(new UserRepository(), new FriendRepository())
    {
    }

    /// <summary>
    /// Регистрация нового пользователя.
    /// Проверяет входные данные, выполняет валидацию и сохраняет пользователя в базе данных.
    /// </summary>
    /// <param name="userRegistrationData">Данные для регистрации пользователя.</param>
    /// <exception cref="ArgumentNullException">Выбрасывается, если одно из обязательных полей отсутствует или не проходит валидацию.</exception>
    /// <exception cref="Exception">Выбрасывается, если не удалось создать пользователя.</exception>
    public void Register(UserRegistrationData userRegistrationData)
    {
        // Проверка, что имя не пустое
        if (string.IsNullOrEmpty(userRegistrationData.FirstName))
            throw new ArgumentNullException(nameof(userRegistrationData.FirstName), "Имя не может быть пустым.");

        // Проверка, что фамилия не пустая
        if (string.IsNullOrEmpty(userRegistrationData.LastName))
            throw new ArgumentNullException(nameof(userRegistrationData.LastName), "Фамилия не может быть пустой.");

        // Проверка, что пароль не пустой
        if (string.IsNullOrEmpty(userRegistrationData.Password))
            throw new ArgumentNullException(nameof(userRegistrationData.Password), "Пароль не может быть пустым.");

        // Проверка, что email не пустой
        if (string.IsNullOrEmpty(userRegistrationData.Email))
            throw new ArgumentNullException(nameof(userRegistrationData.Email), "Email не может быть пустым.");

        // Проверка длины пароля
        if (userRegistrationData.Password.Length < 8)
            throw new ArgumentException("Пароль должен содержать не менее 8 символов.", nameof(userRegistrationData.Password));

        // Проверка корректности email
        if (!new EmailAddressAttribute().IsValid(userRegistrationData.Email))
            throw new ArgumentException("Некорректный email.", nameof(userRegistrationData.Email));

        // Проверка, существует ли уже пользователь с таким email
        if (userRepository.FindByEmail(userRegistrationData.Email) != null)
            throw new ArgumentException("Пользователь с таким email уже зарегистрирован.", nameof(userRegistrationData.Email));

        // Создание сущности пользователя
        var userEntity = new UserEntity
        {
            Firstname = userRegistrationData.FirstName,
            Lastname = userRegistrationData.LastName,
            Password = userRegistrationData.Password,
            Email = userRegistrationData.Email
        };

        // Сохранение пользователя в базе данных
        if (userRepository.Create(userEntity) == 0)
            throw new InvalidOperationException("Не удалось создать пользователя.");
    }


    /// <summary>
    /// Аутентификация пользователя по email и паролю.
    /// </summary>
    /// <param name="userAuthenticationData">Данные для аутентификации пользователя.</param>
    /// <returns>Объект пользователя.</returns>
    /// <exception cref="UserNotFoundException">Выбрасывается, если пользователь не найден.</exception>
    /// <exception cref="WrongPasswordException">Выбрасывается, если пароль неверный.</exception>
    public User Authenticate(UserAuthenticationData userAuthenticationData)
    {
        // Поиск пользователя в базе данных по предоставленному email
        var findUserEntity = userRepository.FindByEmail(userAuthenticationData.Email);

        // Если пользователь с таким email не найден, выбрасываем исключение
        if (findUserEntity is null) throw new UserNotFoundException();

        // Проверка пароля: если введённый пароль не совпадает с паролем в базе данных, выбрасываем исключение
        if (findUserEntity.Password != userAuthenticationData.Password)
            throw new WrongPasswordException();

        // Возвращаем модель пользователя, преобразованную из сущности
        return ConstructUserModel(findUserEntity);
    }

    /// <summary>
    /// Поиск пользователя по email.
    /// </summary>
    /// <param name="email">Email пользователя.</param>
    /// <returns>Объект пользователя.</returns>
    /// <exception cref="UserNotFoundException">Выбрасывается, если пользователь не найден.</exception>
    public User FindByEmail(string? email)
    {
        // Выполняем поиск пользователя в репозитории по электронной почте
        var findUserEntity = userRepository.FindByEmail(email);

        // Если пользователь не найден, выбрасываем исключение
        if (findUserEntity is null) throw new UserNotFoundException();

        // Возвращаем модель пользователя, преобразованную из сущности
        return ConstructUserModel(findUserEntity);
    }

    /// <summary>
    /// Поиск пользователя по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <returns>Объект пользователя.</returns>
    /// <exception cref="UserNotFoundException">Выбрасывается, если пользователь не найден.</exception>
    public User FindById(int id)
    {
        // Выполняем поиск пользователя в репозитории по электронной почте
        var findUserEntity = userRepository.FindById(id);

        // Если пользователь не найден, выбрасываем исключение
        if (findUserEntity is null) throw new UserNotFoundException();

        // Возвращаем модель пользователя, преобразованную из сущности
        return ConstructUserModel(findUserEntity);
    }

    /// <summary>
    /// Обновление данных пользователя.
    /// </summary>
    /// <param name="user">Объект пользователя с обновленными данными.</param>
    /// <exception cref="Exception">Выбрасывается, если обновление не удалось.</exception>
    public void Update(User? user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user), "Пользователь не может быть null.");
        var updatableUserEntity = new UserEntity
        {
            Id = user.Id, // Копируем идентификатор пользователя
            Firstname = user.FirstName, // Копируем имя пользователя
            Lastname = user.LastName, // Копируем фамилию пользователя
            Password = user.Password, // Копируем пароль
            Email = user.Email, // Копируем email
            Photo = user.Photo, // Копируем фото пользователя
            FavoriteMovie = user.FavoriteMovie, // Копируем любимый фильм пользователя
            FavoriteBook = user.FavoriteBook // Копируем любимую книгу пользователя
        };

        // Пытаемся обновить пользователя в репозитории
        if (userRepository.Update(updatableUserEntity) == 0)
            throw new InvalidOperationException("Не удалось обновить пользователя."); // Если обновление не произошло, выбрасываем исключение
    }

    /// <summary>
    /// Получение списка друзей пользователя по его идентификатору.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <returns>Коллекция друзей пользователя.</returns>
    private IEnumerable<User?> GetFriendsByUserId(int userId)
    {
        // Получаем список сущностей друзей по идентификатору пользователя
        return friendRepository.FindAllByUserId(userId)
            // Преобразуем каждую сущность друга в объект User, находя его по идентификатору
            .Select(friendsEntity => FindById(friendsEntity.FriendId));
    }

    /// <summary>
    /// Добавление друга пользователю.
    /// </summary>
    /// <param name="userAddingFriendData">Данные для добавления друга.</param>
    /// <exception cref="UserNotFoundException">Выбрасывается, если пользователь не найден.</exception>
    /// <exception cref="Exception">Выбрасывается, если не удалось добавить друга.</exception>
    public void AddFriend(UserAddingFriendData userAddingFriendData)
    {
        // Ищем пользователя в репозитории по его email
        var findUserEntity = userRepository.FindByEmail(userAddingFriendData.FriendEmail);

        // Если пользователь не найден, выбрасываем исключение
        if (findUserEntity is null) throw new UserNotFoundException();

        // Создаем новую сущность "друг" с указанными идентификаторами
        var friendEntity = new FriendEntity
        {
            UserId = userAddingFriendData.UserId, // ID текущего пользователя
            FriendId = findUserEntity.Id // ID найденного друга
        };

        // Добавляем запись о дружбе в репозиторий
        // Если операция не удалась, выбрасываем исключение
        if (friendRepository.Create(friendEntity) == 0)
            throw new InvalidOperationException("Не удалось добавить друга.");
    }


    /// <summary>
    /// Создание объекта пользователя из сущности базы данных.
    /// </summary>
    /// <param name="userEntity">Сущность пользователя.</param>
    /// <returns>Объект пользователя.</returns>
    private User ConstructUserModel(UserEntity userEntity)
    {
        // Получаем входящие сообщения пользователя
        var incomingMessages = _messageService.GetIncomingMessagesByUserId(userEntity.Id);

        // Получаем исходящие сообщения пользователя
        var outgoingMessages = _messageService.GetOutcomingMessagesByUserId(userEntity.Id);

        // Получаем список друзей пользователя
        var friends = GetFriendsByUserId(userEntity.Id);

        // Создаем и возвращаем объект пользователя с заполненными полями
        return new User(userEntity.Id,
            userEntity.Firstname,
            userEntity.Lastname,
            userEntity.Password,
            userEntity.Email,
            userEntity.Photo,
            userEntity.FavoriteMovie,
            userEntity.FavoriteBook,
            incomingMessages,
            outgoingMessages,
            friends);
    }

}