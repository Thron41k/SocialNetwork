namespace SocialNetwork.BLL.Exceptions;

/// <summary>
/// Исключение, которое выбрасывается, когда введен неправильный пароль.
/// </summary>
/// <remarks>
/// Этот класс предназначен для использования в ситуациях, когда пользователь вводит неверный пароль,
/// например, при попытке авторизации или изменении пароля.
/// </remarks>
public class WrongPasswordException : Exception
{
    /// <summary>
    /// Инициализирует новый экземпляр исключения <see cref="WrongPasswordException"/>.
    /// </summary>
    public WrongPasswordException()
    {
    }

    /// <summary>
    /// Инициализирует новый экземпляр исключения <see cref="WrongPasswordException"/> с сообщением об ошибке.
    /// </summary>
    /// <param name="message">Сообщение, которое описывает ошибку.</param>
    public WrongPasswordException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Инициализирует новый экземпляр исключения <see cref="WrongPasswordException"/> с сообщением об ошибке
    /// и внутренним исключением, которое вызвало это исключение.
    /// </summary>
    /// <param name="message">Сообщение, которое описывает ошибку.</param>
    /// <param name="innerException">Внутреннее исключение, которое вызвало текущее исключение.</param>
    public WrongPasswordException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}