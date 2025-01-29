namespace SocialNetwork.BLL.Exceptions;

public class UserNotFoundException(string? email) : Exception
{
    public string? Email { get; } = email;
}