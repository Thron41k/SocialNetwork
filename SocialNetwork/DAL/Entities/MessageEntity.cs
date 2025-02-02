namespace SocialNetwork.DAL.Entities;

/// <summary>
/// Представляет сущность сообщения в системе.
/// </summary>
public class MessageEntity
{
    /// <summary>
    /// Уникальный идентификатор сообщения.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Текстовое содержимое сообщения.
    /// </summary>
    public string? Content { get; init; }

    /// <summary>
    /// Идентификатор пользователя, отправившего сообщение.
    /// </summary>
    public int SenderId { get; init; }

    /// <summary>
    /// Идентификатор пользователя, получателя сообщения.
    /// </summary>
    public int RecipientId { get; init; }
}