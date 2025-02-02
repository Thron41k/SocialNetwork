namespace SocialNetwork.BLL.Models;

/// <summary>
/// Класс, содержащий данные для аутентификации пользователя.
/// </summary>
public class UserAuthenticationData
{
    /// <summary>
    /// Адрес электронной почты пользователя.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Пароль пользователя.
    /// </summary>
    public string? Password { get; set; }
}