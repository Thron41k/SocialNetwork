namespace SocialNetwork.DAL.Entities;

/// <summary>
/// Представляет сущность дружбы между пользователями.
/// </summary>
public class FriendEntity
{
    /// <summary>
    /// Уникальный идентификатор записи дружбы.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор пользователя, который добавил друга.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Идентификатор друга.
    /// </summary>
    public int FriendId { get; init; }
}
