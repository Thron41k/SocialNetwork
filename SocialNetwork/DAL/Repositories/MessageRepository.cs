using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories.Interfaces;

namespace SocialNetwork.DAL.Repositories;

/// <summary>
/// Репозиторий для работы с сущностью сообщений в базе данных.
/// Наследуется от <see cref="BaseRepository"/> и реализует интерфейс <see cref="IMessageRepository"/>.
/// </summary>
public class MessageRepository : BaseRepository, IMessageRepository
{
    /// <summary>
    /// Создает новое сообщение в базе данных.
    /// </summary>
    /// <param name="messageEntity">Сущность сообщения, содержащая информацию о содержимом, отправителе и получателе.</param>
    /// <returns>Возвращает количество затронутых строк (обычно 1, если вставка успешна).</returns>
    public int Create(MessageEntity messageEntity)
    {
        // Выполняет SQL-запрос на вставку нового сообщения в таблицу messages
        return Execute(@"insert into messages(content, senderId, recipientId) 
                             values(:Content,:SenderId,:RecipientId)", messageEntity);
    }

    /// <summary>
    /// Ищет все сообщения, отправленные указанным пользователем по его идентификатору.
    /// </summary>
    /// <param name="senderId">Идентификатор отправителя.</param>
    /// <returns>Коллекция сообщений, отправленных пользователем с указанным идентификатором.</returns>
    public IEnumerable<MessageEntity> FindBySenderId(int senderId)
    {
        // Выполняет SQL-запрос на поиск сообщений по идентификатору отправителя
        return Query<MessageEntity>("select * from messages where senderId = :sender_id", new { sender_id = senderId });
    }

    /// <summary>
    /// Ищет все сообщения, полученные указанным пользователем по его идентификатору.
    /// </summary>
    /// <param name="recipientId">Идентификатор получателя.</param>
    /// <returns>Коллекция сообщений, полученных пользователем с указанным идентификатором.</returns>
    public IEnumerable<MessageEntity> FindByRecipientId(int recipientId)
    {
        // Выполняет SQL-запрос на поиск сообщений по идентификатору получателя
        return Query<MessageEntity>("select * from messages where recipientId = :recipient_id", new { recipient_id = recipientId });
    }

    /// <summary>
    /// Удаляет сообщение из базы данных по его идентификатору.
    /// </summary>
    /// <param name="messageId">Идентификатор сообщения, которое необходимо удалить.</param>
    /// <returns>Возвращает количество затронутых строк (обычно 1, если удаление успешно).</returns>
    public int DeleteById(int messageId)
    {
        // Выполняет SQL-запрос на удаление сообщения по его идентификатору
        return Execute("delete from messages where id = :id", new { id = messageId });
    }
}
