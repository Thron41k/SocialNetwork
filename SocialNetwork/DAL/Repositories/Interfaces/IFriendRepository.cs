using SocialNetwork.DAL.Entities;

namespace SocialNetwork.DAL.Repositories.Interfaces;

/// <summary>
/// Интерфейс репозитория для управления друзьями.
/// Предоставляет методы для создания, поиска и удаления записей о друзьях.
/// </summary>
public interface IFriendRepository
{
    /// <summary>
    /// Создает нового друга в базе данных.
    /// </summary>
    /// <param name="friendEntity">Объект, представляющий данные друга, которые нужно создать.</param>
    /// <returns>Возвращает идентификатор созданного друга.</returns>
    int Create(FriendEntity friendEntity);

    /// <summary>
    /// Находит всех друзей для указанного пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя, для которого нужно найти друзей.</param>
    /// <returns>Коллекция объектов <see cref="FriendEntity"/>, представляющих друзей пользователя.</returns>
    IEnumerable<FriendEntity> FindAllByUserId(int userId);

    /// <summary>
    /// Удаляет друга по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор друга, которого необходимо удалить.</param>
    /// <returns>Возвращает количество удалённых записей (обычно 1, если друг найден и удален, 0, если запись не найдена).</returns>
    int Delete(int id);
}
