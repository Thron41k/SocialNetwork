namespace SocialNetwork.BLL.Models;

/// <summary>
/// Класс, содержащий данные для регистрации пользователя.
/// </summary>
public class UserRegistrationData
{
    /// <summary>
    /// Имя пользователя.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Фамилия пользователя.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Пароль пользователя.
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// Адрес электронной почты пользователя.
    /// </summary>
    public string? Email { get; set; }
}