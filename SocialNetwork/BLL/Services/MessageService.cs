using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories;
using SocialNetwork.DAL.Repositories.Interfaces;

namespace SocialNetwork.BLL.Services;

/// <summary>
/// Сервис для управления сообщениями между пользователями.
/// </summary>
public class MessageService(IMessageRepository messageRepository, IUserRepository userRepository)
{
    /// <summary>
    /// Конструктор по умолчанию, использующий стандартные реализации репозиториев.
    /// </summary>
    public MessageService() : this(new MessageRepository(), new UserRepository())
    {
    }

    /// <summary>
    /// Получает входящие сообщения пользователя по его идентификатору.
    /// </summary>
    /// <param name="recipientId">Идентификатор получателя сообщений.</param>
    /// <returns>Коллекция входящих сообщений.</returns>
    public IEnumerable<Message> GetIncomingMessagesByUserId(int recipientId)
    {
        // Создаем список для хранения сообщений
        var messages = new List<Message>();

        // Ищем все сообщения, полученные пользователем с указанным идентификатором
        messageRepository.FindByRecipientId(recipientId).ToList().ForEach(m =>
        {
            // Получаем пользователя-отправителя по его идентификатору
            var senderUserEntity = userRepository.FindById(m.SenderId);

            // Получаем пользователя-получателя по его идентификатору
            var recipientUserEntity = userRepository.FindById(m.RecipientId);

            // Добавляем новое сообщение в список, с данными о сообщении и пользователях (отправитель и получатель)
            messages.Add(new Message(m.Id, m.Content, senderUserEntity?.Email, recipientUserEntity?.Email));
        });

        // Возвращаем список входящих сообщений
        return messages;
    }

    /// <summary>
    /// Получает исходящие сообщения пользователя по его идентификатору.
    /// </summary>
    /// <param name="senderId">Идентификатор отправителя сообщений.</param>
    /// <returns>Коллекция исходящих сообщений.</returns>
    public IEnumerable<Message> GetOutcomingMessagesByUserId(int senderId)
    {
        // Создаем список для хранения сообщений
        var messages = new List<Message>();

        // Ищем все сообщения, отправленные пользователем с указанным идентификатором
        messageRepository.FindBySenderId(senderId).ToList().ForEach(m =>
        {
            // Получаем пользователя-отправителя по его идентификатору
            var senderUserEntity = userRepository.FindById(m.SenderId);

            // Получаем пользователя-получателя по его идентификатору
            var recipientUserEntity = userRepository.FindById(m.RecipientId);

            // Добавляем новое сообщение в список, с данными о сообщении и пользователях (отправитель и получатель)
            messages.Add(new Message(m.Id, m.Content, senderUserEntity?.Email, recipientUserEntity?.Email));
        });

        // Возвращаем список исходящих сообщений
        return messages;
    }

    /// <summary>
    /// Отправляет сообщение от одного пользователя другому.
    /// </summary>
    /// <param name="messageSendingData">Данные для отправки сообщения.</param>
    /// <exception cref="ArgumentNullException">Выбрасывается, если содержание сообщения пустое.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Выбрасывается, если сообщение превышает допустимую длину.</exception>
    /// <exception cref="UserNotFoundException">Выбрасывается, если получатель сообщения не найден.</exception>
    /// <exception cref="Exception">Выбрасывается, если не удалось отправить сообщение.</exception>
    public void SendMessage(MessageSendingData messageSendingData)
    {
        // Проверка на пустое содержание сообщения
        if (string.IsNullOrEmpty(messageSendingData.Content))
            throw new ArgumentNullException(nameof(messageSendingData.Content), "Сообщение не может быть пустым.");

        // Проверка на превышение максимальной длины сообщения (5000 символов)
        if (messageSendingData.Content.Length > 5000)
            throw new ArgumentOutOfRangeException(nameof(messageSendingData.Content), "Сообщение не может превышать 5000 символов.");

        // Поиск получателя сообщения по email
        var findUserEntity = userRepository.FindByEmail(messageSendingData.RecipientEmail);

        // Если получатель не найден, выбрасывается исключение
        if (findUserEntity is null) throw new UserNotFoundException();

        // Создание сущности сообщения с необходимыми данными
        var messageEntity = new MessageEntity
        {
            Content = messageSendingData.Content,
            SenderId = messageSendingData.SenderId,  // Идентификатор отправителя
            RecipientId = findUserEntity.Id        // Идентификатор получателя
        };

        // Попытка сохранения сообщения в базе данных
        if (messageRepository.Create(messageEntity) == 0)
            // Если сообщение не было сохранено, выбрасывается исключение
            throw new InvalidOperationException("Не удалось отправить сообщение.");
    }
}
