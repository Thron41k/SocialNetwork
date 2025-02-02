namespace SocialNetwork.BLL.Models;

/// <summary>
/// Данные, необходимые для добавления друга пользователю.
/// </summary>
public class UserAddingFriendData
{
    /// <summary>
    /// Идентификатор пользователя, который добавляет друга.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Email друга, которого добавляет пользователь.
    /// </summary>
    public string? FriendEmail { get; set; }
}
