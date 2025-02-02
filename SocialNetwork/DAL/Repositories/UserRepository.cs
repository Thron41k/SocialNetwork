using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories.Interfaces;

namespace SocialNetwork.DAL.Repositories;


/// <summary>
/// Репозиторий для работы с сущностью пользователя в базе данных.
/// Наследуется от <see cref="BaseRepository"/> и реализует интерфейс <see cref="IUserRepository"/>.
/// </summary>
public class UserRepository : BaseRepository, IUserRepository
{
    /// <summary>
    /// Создает нового пользователя в базе данных.
    /// </summary>
    /// <param name="userEntity">Объект, содержащий данные пользователя для добавления.</param>
    /// <returns>Количество затронутых строк в базе данных (обычно 1, если вставка прошла успешно).</returns>
    public int Create(UserEntity userEntity)
    {
        // Выполняет SQL-запрос на добавление нового пользователя в таблицу users
        return Execute(@"insert into users (firstname,lastname,password,email) 
                             values (:Firstname,:Lastname,:Password,:Email)", userEntity);
    }

    /// <summary>
    /// Находит всех пользователей в базе данных.
    /// </summary>
    /// <returns>Перечень всех пользователей в базе данных.</returns>
    public IEnumerable<UserEntity> FindAll()
    {
        // Выполняет SQL-запрос для получения всех записей из таблицы users
        return Query<UserEntity>("select * from users");
    }

    /// <summary>
    /// Находит пользователя по его электронному адресу.
    /// </summary>
    /// <param name="email">Электронный адрес пользователя, которого необходимо найти.</param>
    /// <returns>Объект пользователя с указанным адресом, если он существует, или null, если пользователь не найден.</returns>
    public UserEntity? FindByEmail(string? email)
    {
        // Выполняет SQL-запрос для поиска пользователя по электронному адресу
        return QueryFirstOrDefault<UserEntity>("select * from users where email = :email_p", new { email_p = email });
    }

    /// <summary>
    /// Находит пользователя по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор пользователя, которого необходимо найти.</param>
    /// <returns>Объект пользователя с указанным идентификатором, если он существует, или null, если пользователь не найден.</returns>
    public UserEntity? FindById(int id)
    {
        // Выполняет SQL-запрос для поиска пользователя по его идентификатору
        return QueryFirstOrDefault<UserEntity>("select * from users where id = :id_p", new { id_p = id });
    }

    /// <summary>
    /// Обновляет информацию о пользователе в базе данных.
    /// </summary>
    /// <param name="userEntity">Объект пользователя, содержащий обновленные данные.</param>
    /// <returns>Количество затронутых строк в базе данных (обычно 1, если обновление прошло успешно).</returns>
    public int Update(UserEntity userEntity)
    {
        // Выполняет SQL-запрос для обновления данных пользователя в таблице users
        return Execute(@"update users set firstname = :Firstname, lastname = :Lastname, password = :Password, email = :Email,
                             photo = :Photo, favoriteMovie = :FavoriteMovie, favoriteBook = :FavoriteBook where id = :Id", userEntity);
    }

    /// <summary>
    /// Удаляет пользователя из базы данных по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор пользователя, которого необходимо удалить.</param>
    /// <returns>Количество затронутых строк в базе данных (обычно 1, если удаление прошло успешно).</returns>
    public int DeleteById(int id)
    {
        // Выполняет SQL-запрос для удаления пользователя по его идентификатору
        return Execute("delete from users where id = :id_p", new { id_p = id });
    }
}
