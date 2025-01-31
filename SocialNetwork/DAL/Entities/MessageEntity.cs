namespace SocialNetwork.DAL.Entities;

public class MessageEntity
{
    public int Id { get; set; }
    public string? Content { get; init; }
    public int SenderId { get; init; }
    public int RecipientId { get; init; }
}