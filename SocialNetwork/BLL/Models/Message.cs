namespace SocialNetwork.BLL.Models;

public class Message(int id, string? content, string? senderEmail, string? recipientEmail)
{
    public int Id { get; } = id;
    public string? Content { get; } = content;
    public string? SenderEmail { get; } = senderEmail;
    public string? RecipientEmail { get; } = recipientEmail;
}