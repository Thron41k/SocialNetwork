namespace SocialNetwork.BLL.Models;

/// <summary>
/// Представляет пользователя в системе.
/// </summary>
public class User(
    int id,
    string? firstName,
    string? lastName,
    string? password,
    string? email,
    string? photo,
    string? favoriteMovie,
    string? favoriteBook,
    IEnumerable<Message> incomingMessages,
    IEnumerable<Message> outgoingMessages,
    IEnumerable<User?> friends)
{
    /// <summary>
    /// Уникальный идентификатор пользователя.
    /// </summary>
    public int Id { get; } = id;

    /// <summary>
    /// Имя пользователя.
    /// </summary>
    public string? FirstName { get; set; } = firstName;

    /// <summary>
    /// Фамилия пользователя.
    /// </summary>
    public string? LastName { get; set; } = lastName;

    /// <summary>
    /// Пароль пользователя.
    /// </summary>
    public string? Password { get; set; } = password;

    /// <summary>
    /// Адрес электронной почты пользователя.
    /// </summary>
    public string? Email { get; set; } = email;

    /// <summary>
    /// Фотография пользователя.
    /// </summary>
    public string? Photo { get; set; } = photo;

    /// <summary>
    /// Любимый фильм пользователя.
    /// </summary>
    public string? FavoriteMovie { get; set; } = favoriteMovie;

    /// <summary>
    /// Любимая книга пользователя.
    /// </summary>
    public string? FavoriteBook { get; set; } = favoriteBook;

    /// <summary>
    /// Входящие сообщения пользователя.
    /// </summary>
    public IEnumerable<Message> IncomingMessages { get; } = incomingMessages;

    /// <summary>
    /// Исходящие сообщения пользователя.
    /// </summary>
    public IEnumerable<Message> OutgoingMessages { get; } = outgoingMessages;

    /// <summary>
    /// Друзья пользователя.
    /// </summary>
    public IEnumerable<User?> Friends { get; } = friends;
}
