using SocialNetwork.DAL.Entities;

namespace SocialNetwork.DAL.Repositories.Interfaces;

/// <summary>
/// Интерфейс репозитория для работы с пользователями.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Создает нового пользователя в хранилище.
    /// </summary>
    /// <param name="userEntity">Сущность пользователя, которая будет создана.</param>
    /// <returns>Возвращает идентификатор созданного пользователя.</returns>
    int Create(UserEntity userEntity);

    /// <summary>
    /// Находит пользователя по адресу электронной почты.
    /// </summary>
    /// <param name="email">Электронная почта пользователя.</param>
    /// <returns>Возвращает сущность пользователя, если пользователь найден, или null, если нет.</returns>
    UserEntity? FindByEmail(string? email);

    /// <summary>
    /// Получает всех пользователей из хранилища.
    /// </summary>
    /// <returns>Коллекция сущностей пользователей.</returns>
    IEnumerable<UserEntity> FindAll();

    /// <summary>
    /// Находит пользователя по уникальному идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <returns>Возвращает сущность пользователя, если пользователь найден, или null, если нет.</returns>
    UserEntity? FindById(int id);

    /// <summary>
    /// Обновляет информацию о пользователе в хранилище.
    /// </summary>
    /// <param name="userEntity">Сущность пользователя с обновленными данными.</param>
    /// <returns>Возвращает количество затронутых строк в хранилище (обычно 1 для успешного обновления).</returns>
    int Update(UserEntity userEntity);

    /// <summary>
    /// Удаляет пользователя из хранилища по уникальному идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор пользователя, которого необходимо удалить.</param>
    /// <returns>Возвращает количество затронутых строк в хранилище (обычно 1 для успешного удаления).</returns>
    int DeleteById(int id);
}
