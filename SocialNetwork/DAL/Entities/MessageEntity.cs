namespace SocialNetwork.DAL.Entities;

public class MessageEntity
{
    public int Id { get; set; }
    public string? Content { get; set; }
    public int SenderId { get; set; }
    public int RecipientId { get; set; }
}