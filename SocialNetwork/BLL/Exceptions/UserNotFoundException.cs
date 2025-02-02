namespace SocialNetwork.BLL.Exceptions;

/// <summary>
/// Исключение, выбрасываемое, когда пользователь не найден.
/// </summary>
/// <remarks>
/// Этот класс наследует от базового класса <see cref="Exception"/> и используется для обработки случаев,
/// когда в системе не удается найти пользователя по заданному идентификатору или другим параметрам.
/// </remarks>
public class UserNotFoundException : Exception
{
    /// <summary>
    /// Инициализирует новое исключение с сообщением по умолчанию.
    /// </summary>
    public UserNotFoundException()
        : base("Пользователь не найден.")
    {
    }

    /// <summary>
    /// Инициализирует новое исключение с заданным сообщением.
    /// </summary>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    public UserNotFoundException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Инициализирует новое исключение с заданным сообщением и внутренним исключением.
    /// </summary>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    /// <param name="innerException">Исключение, которое вызвало данное исключение.</param>
    public UserNotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}