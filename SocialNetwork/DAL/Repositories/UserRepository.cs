using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories.Interfaces;

namespace SocialNetwork.DAL.Repositories;

public class UserRepository : BaseRepository, IUserRepository
{
    public int Create(UserEntity userEntity)
    {
        return Execute(@"insert into users (firstname,lastname,password,email) 
                             values (:Firstname,:Lastname,:Password,:Email)", userEntity);
    }

    public IEnumerable<UserEntity> FindAll()
    {
        return Query<UserEntity>("select * from users");
    }

    public UserEntity? FindByEmail(string? email)
    {
        return QueryFirstOrDefault<UserEntity>("select * from users where email = :email_p", new { email_p = email });
    }

    public UserEntity? FindById(int id)
    {
        return QueryFirstOrDefault<UserEntity>("select * from users where id = :id_p", new { id_p = id });
    }

    public int Update(UserEntity userEntity)
    {
        return Execute(@"update users set firstname = :Firstname, lastname = :Lastname, password = :Password, email = :Email,
                             photo = :Photo, favoriteMovie = :FavoriteMovie, favoriteBook = :FavoriteBook where id = :Id", userEntity);
    }

    public int DeleteById(int id)
    {
        return Execute("delete from users where id = :id_p", new { id_p = id });
    }
}