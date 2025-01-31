namespace SocialNetwork.BLL.Models;

public class User(
    int id,
    string? firstName,
    string? lastName,
    string? password,
    string? email,
    string? photo,
    string? favoriteMovie,
    string? favoriteBook, 
    IEnumerable<Message> incomingMessages,
    IEnumerable<Message> outgoingMessages,
    IEnumerable<User?> friends)
{
    public int Id { get; } = id;
    public string? FirstName { get; set; } = firstName;
    public string? LastName { get; set; } = lastName;
    public string? Password { get; set; } = password;
    public string? Email { get; set; } = email;
    public string? Photo { get; set; } = photo;
    public string? FavoriteMovie { get; set; } = favoriteMovie;
    public string? FavoriteBook { get; set; } = favoriteBook;
    public IEnumerable<Message> IncomingMessages { get; } = incomingMessages;
    public IEnumerable<Message> OutgoingMessages { get; } = outgoingMessages;
    public IEnumerable<User?> Friends { get; } = friends;
}