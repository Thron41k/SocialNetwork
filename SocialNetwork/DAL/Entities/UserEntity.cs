namespace SocialNetwork.DAL.Entities;
public class UserEntity
{
    public int Id { get; set; }
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
    public string? Photo { get; set; }
    public string? FavoriteMovie { get; set; }
    public string? FavoriteBook { get; set; }
}
