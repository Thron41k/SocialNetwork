namespace SocialNetwork.BLL.Models;

/// <summary>
/// Класс, представляющий данные для отправки сообщения.
/// </summary>
public class MessageSendingData
{
    /// <summary>
    /// Идентификатор отправителя сообщения.
    /// </summary>
    public int SenderId { get; set; }

    /// <summary>
    /// Содержимое сообщения.
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    /// Email получателя сообщения.
    /// </summary>
    public string? RecipientEmail { get; set; }
}