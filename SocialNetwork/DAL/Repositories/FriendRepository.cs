using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories.Interfaces;

namespace SocialNetwork.DAL.Repositories;

/// <summary>
/// Репозиторий для работы с сущностью друзей.
/// Наследуется от <see cref="BaseRepository"/> и реализует интерфейс <see cref="IFriendRepository"/>.
/// </summary>
public class FriendRepository : BaseRepository, IFriendRepository
{
    /// <summary>
    /// Находит всех друзей по ID пользователя.
    /// </summary>
    /// <param name="userId">ID пользователя, для которого нужно найти друзей.</param>
    /// <returns>Коллекция сущностей друзей <see cref="FriendEntity"/> для указанного пользователя.</returns>
    public IEnumerable<FriendEntity> FindAllByUserId(int userId)
    {
        // Выполняем SQL-запрос для получения всех друзей по userId.
        return Query<FriendEntity>(@"select * from friends where userId = :user_id", new { user_id = userId });
    }

    /// <summary>
    /// Создает новую запись о друге.
    /// </summary>
    /// <param name="friendEntity">Сущность друга, содержащая данные для вставки в базу данных.</param>
    /// <returns>Количество затронутых строк в базе данных.</returns>
    public int Create(FriendEntity friendEntity)
    {
        // Выполняем SQL-запрос для вставки данных о новом друге в таблицу friends.
        return Execute(@"insert into friends (userId, friendId) values (:UserId, :FriendId)", friendEntity);
    }

    /// <summary>
    /// Удаляет запись о друге по указанному ID.
    /// </summary>
    /// <param name="id">ID записи о друге, которую нужно удалить.</param>
    /// <returns>Количество затронутых строк в базе данных.</returns>
    public int Delete(int id)
    {
        // Выполняем SQL-запрос для удаления записи о друге по заданному ID.
        return Execute(@"delete from friends where id = :id_p", new { id_p = id });
    }
}
