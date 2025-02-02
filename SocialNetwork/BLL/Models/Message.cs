namespace SocialNetwork.BLL.Models;

/// <summary>
/// Класс, представляющий сообщение в системе
/// </summary>
/// <param name="id">Идентификатор сообщения.</param>
/// <param name="content">Содержимое сообщения.</param>
/// <param name="senderEmail">Электронная почта отправителя.</param>
/// <param name="recipientEmail">Электронная почта получателя.</param>
public class Message(int id, string? content, string? senderEmail, string? recipientEmail)
{
    /// <summary>
    /// Идентификатор сообщения.
    /// </summary>
    public int Id { get; } = id;

    /// <summary>
    /// Содержимое сообщения.
    /// </summary>
    public string? Content { get; } = content;

    /// <summary>
    /// Электронная почта отправителя.
    /// </summary>
    public string? SenderEmail { get; } = senderEmail;

    /// <summary>
    /// Электронная почта  получателя.
    /// </summary>
    public string? RecipientEmail { get; } = recipientEmail;
}