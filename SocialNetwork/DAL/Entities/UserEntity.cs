namespace SocialNetwork.DAL.Entities;

/// <summary>
/// Представляет сущность пользователя в базе данных.
/// </summary>
public class UserEntity
{
    /// <summary>
    /// Уникальный идентификатор пользователя.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Имя пользователя.
    /// </summary>
    public string? Firstname { get; init; }

    /// <summary>
    /// Фамилия пользователя.
    /// </summary>
    public string? Lastname { get; init; }

    /// <summary>
    /// Пароль пользователя.
    /// </summary>
    public string? Password { get; init; }

    /// <summary>
    /// Электронная почта пользователя.
    /// </summary>
    public string? Email { get; init; }

    /// <summary>
    /// Ссылка на фотографию пользователя.
    /// </summary>
    public string? Photo { get; init; }

    /// <summary>
    /// Любимый фильм пользователя.
    /// </summary>
    public string? FavoriteMovie { get; init; }

    /// <summary>
    /// Любимая книга пользователя.
    /// </summary>
    public string? FavoriteBook { get; init; }
}
