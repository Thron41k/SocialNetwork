namespace SocialNetwork.DAL.Entities;
public class UserEntity
{
    public int Id { get; init; }
    public string? Firstname { get; init; }
    public string? Lastname { get; init; }
    public string? Password { get; init; }
    public string? Email { get; init; }
    public string? Photo { get; init; }
    public string? FavoriteMovie { get; init; }
    public string? FavoriteBook { get; init; }
}
