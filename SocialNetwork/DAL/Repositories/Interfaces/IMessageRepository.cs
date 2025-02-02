using SocialNetwork.DAL.Entities;

namespace SocialNetwork.DAL.Repositories.Interfaces;

/// <summary>
/// Интерфейс для репозитория сообщений, предоставляющий операции для работы с сущностью сообщений.
/// </summary>
public interface IMessageRepository
{
    /// <summary>
    /// Создает новое сообщение в хранилище.
    /// </summary>
    /// <param name="messageEntity">Сущность сообщения, которая будет создана.</param>
    /// <returns>Идентификатор созданного сообщения.</returns>
    int Create(MessageEntity messageEntity);

    /// <summary>
    /// Находит все сообщения, отправленные пользователем с указанным идентификатором.
    /// </summary>
    /// <param name="senderId">Идентификатор отправителя сообщений.</param>
    /// <returns>Коллекция сообщений, отправленных указанным пользователем.</returns>
    IEnumerable<MessageEntity> FindBySenderId(int senderId);

    /// <summary>
    /// Находит все сообщения, полученные пользователем с указанным идентификатором.
    /// </summary>
    /// <param name="recipientId">Идентификатор получателя сообщений.</param>
    /// <returns>Коллекция сообщений, полученных указанным пользователем.</returns>
    IEnumerable<MessageEntity> FindByRecipientId(int recipientId);

    /// <summary>
    /// Удаляет сообщение по его идентификатору.
    /// </summary>
    /// <param name="messageId">Идентификатор сообщения, которое нужно удалить.</param>
    /// <returns>Число, указывающее количество удаленных сообщений (0 или 1).</returns>
    int DeleteById(int messageId);
}