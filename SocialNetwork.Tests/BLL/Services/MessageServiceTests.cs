using Moq;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories.Interfaces;
using static NUnit.Framework.Assert;

namespace SocialNetwork.Tests.BLL.Services;

[TestFixture]
public class MessageServiceTests
{
    // Мок-объекты репозиториев для сообщений и пользователей
    private Mock<IMessageRepository> _messageRepositoryMock;
    private Mock<IUserRepository> _userRepositoryMock;
    private MessageService _messageService;

    // Метод для настройки тестового окружения
    [SetUp]
    public void SetUp()
    {
        // Инициализация мок-объектов и сервиса сообщений
        _messageRepositoryMock = new Mock<IMessageRepository>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _messageService = new MessageService(_messageRepositoryMock.Object, _userRepositoryMock.Object);
    }

    // Тест на получение входящих сообщений с подстановкой электронной почты пользователей
    [Test]
    public void GetIncomingMessagesByUserId_ReturnsMessagesWithUserEmails()
    {
        const int recipientId = 1;
        const int senderId = 2;

        // Список сообщений для теста
        var messageEntities = new List<MessageEntity>
        {
            new() { Id = 1, Content = "Hello", SenderId = senderId, RecipientId = recipientId }
        };

        // Создание пользователей с почтовыми адресами
        var senderUserEntity = new UserEntity { Id = senderId, Email = "sender@example.com" };
        var recipientUserEntity = new UserEntity { Id = recipientId, Email = "recipient@example.com" };

        // Настройка моков для репозиториев
        _messageRepositoryMock.Setup(repo => repo.FindByRecipientId(recipientId)).Returns(messageEntities);
        _userRepositoryMock.Setup(repo => repo.FindById(senderId)).Returns(senderUserEntity);
        _userRepositoryMock.Setup(repo => repo.FindById(recipientId)).Returns(recipientUserEntity);

        // Вызов метода сервиса и проверка результатов
        var result = _messageService.GetIncomingMessagesByUserId(recipientId).ToList();
        That(result, Has.Count.EqualTo(1)); // Проверка, что пришло одно сообщение
        That(result[0].Content, Is.EqualTo("Hello"));
        That(result[0].SenderEmail, Is.EqualTo("sender@example.com"));
        That(result[0].RecipientEmail, Is.EqualTo("recipient@example.com"));
    }

    // Тест на получение исходящих сообщений с подстановкой электронной почты пользователей
    [Test]
    public void GetOutcomingMessagesByUserId_ReturnsMessagesWithUserEmails()
    {
        const int senderId = 1;
        const int recipientId = 2;

        // Список сообщений для теста
        var messageEntities = new List<MessageEntity>
        {
            new() { Id = 1, Content = "Hi", SenderId = senderId, RecipientId = recipientId }
        };

        // Создание пользователей с почтовыми адресами
        var senderUserEntity = new UserEntity { Id = senderId, Email = "sender@example.com" };
        var recipientUserEntity = new UserEntity { Id = recipientId, Email = "recipient@example.com" };

        // Настройка моков для репозиториев
        _messageRepositoryMock.Setup(repo => repo.FindBySenderId(senderId)).Returns(messageEntities);
        _userRepositoryMock.Setup(repo => repo.FindById(senderId)).Returns(senderUserEntity);
        _userRepositoryMock.Setup(repo => repo.FindById(recipientId)).Returns(recipientUserEntity);

        // Вызов метода сервиса и проверка результатов
        var result = _messageService.GetOutcomingMessagesByUserId(senderId).ToList();
        That(result, Has.Count.EqualTo(1)); // Проверка, что пришло одно сообщение
        That(result[0].Content, Is.EqualTo("Hi"));
        That(result[0].SenderEmail, Is.EqualTo("sender@example.com"));
        That(result[0].RecipientEmail, Is.EqualTo("recipient@example.com"));
    }

    // Тест на исключение, если контент сообщения пустой
    [Test]
    public void SendMessage_ThrowsArgumentNullException_WhenContentIsNullOrEmpty()
    {
        var messageSendingData = new MessageSendingData { Content = "", SenderId = 1, RecipientEmail = "recipient@example.com" };

        // Ожидаем выброс ArgumentNullException
        Throws<ArgumentNullException>(() => _messageService.SendMessage(messageSendingData));
    }

    // Тест на исключение, если контент сообщения слишком длинный
    [Test]
    public void SendMessage_ThrowsArgumentOutOfRangeException_WhenContentIsTooLong()
    {
        var longContent = new string('a', 5001); // Длина контента больше максимума
        var messageSendingData = new MessageSendingData { Content = longContent, SenderId = 1, RecipientEmail = "recipient@example.com" };

        // Ожидаем выброс ArgumentOutOfRangeException
        Throws<ArgumentOutOfRangeException>(() => _messageService.SendMessage(messageSendingData));
    }

    // Тест на исключение, если получатель не найден
    [Test]
    public void SendMessage_ThrowsUserNotFoundException_WhenRecipientNotFound()
    {
        var messageSendingData = new MessageSendingData { Content = "Hello", SenderId = 1, RecipientEmail = "nonexistent@example.com" };

        // Настройка мока на возврат null для несуществующего пользователя
        _userRepositoryMock.Setup(repo => repo.FindByEmail(messageSendingData.RecipientEmail)).Returns((UserEntity)null!);

        // Ожидаем выброс UserNotFoundException
        Throws<UserNotFoundException>(() => _messageService.SendMessage(messageSendingData));
    }

    // Тест на исключение, если создание сообщения не удалось
    [Test]
    public void SendMessage_ThrowsInvalidOperationException_WhenMessageCreationFails()
    {
        var messageSendingData = new MessageSendingData { Content = "Hello", SenderId = 1, RecipientEmail = "recipient@example.com" };
        var recipientUserEntity = new UserEntity { Id = 2, Email = "recipient@example.com" };

        // Настройка мока на возврат существующего пользователя
        _userRepositoryMock.Setup(repo => repo.FindByEmail(messageSendingData.RecipientEmail)).Returns(recipientUserEntity);

        // Настройка мока на неудачное создание сообщения (возвращается 0)
        _messageRepositoryMock.Setup(repo => repo.Create(It.IsAny<MessageEntity>())).Returns(0);

        // Ожидаем выброс InvalidOperationException
        Throws<InvalidOperationException>(() => _messageService.SendMessage(messageSendingData));
    }

    // Тест на успешное создание сообщения
    [Test]
    public void SendMessage_SuccessfullyCreatesMessage()
    {
        var messageSendingData = new MessageSendingData { Content = "Hello", SenderId = 1, RecipientEmail = "recipient@example.com" };
        var recipientUserEntity = new UserEntity { Id = 2, Email = "recipient@example.com" };

        // Настройка мока на возврат существующего пользователя
        _userRepositoryMock.Setup(repo => repo.FindByEmail(messageSendingData.RecipientEmail)).Returns(recipientUserEntity);

        // Настройка мока на успешное создание сообщения (возвращается 1)
        _messageRepositoryMock.Setup(repo => repo.Create(It.IsAny<MessageEntity>())).Returns(1);

        // Вызов метода и проверка вызова создания сообщения
        _messageService.SendMessage(messageSendingData);
        _messageRepositoryMock.Verify(repo => repo.Create(It.IsAny<MessageEntity>()), Times.Once); // Проверка, что метод вызван один раз
    }
}
