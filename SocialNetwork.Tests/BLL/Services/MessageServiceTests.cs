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
    private Mock<IMessageRepository> _messageRepositoryMock;
    private Mock<IUserRepository> _userRepositoryMock;
    private MessageService _messageService;

    [SetUp]
    public void SetUp()
    {
        _messageRepositoryMock = new Mock<IMessageRepository>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _messageService = new MessageService(_messageRepositoryMock.Object, _userRepositoryMock.Object);
    }

    [Test]
    public void GetIncomingMessagesByUserId_ReturnsMessagesWithUserEmails()
    {
        const int recipientId = 1;
        const int senderId = 2;
        var messageEntities = new List<MessageEntity>
        {
            new() { Id = 1, Content = "Hello", SenderId = senderId, RecipientId = recipientId }
        };
        var senderUserEntity = new UserEntity { Id = senderId, Email = "sender@example.com" };
        var recipientUserEntity = new UserEntity { Id = recipientId, Email = "recipient@example.com" };
        _messageRepositoryMock.Setup(repo => repo.FindByRecipientId(recipientId)).Returns(messageEntities);
        _userRepositoryMock.Setup(repo => repo.FindById(senderId)).Returns(senderUserEntity);
        _userRepositoryMock.Setup(repo => repo.FindById(recipientId)).Returns(recipientUserEntity);
        var result = _messageService.GetIncomingMessagesByUserId(recipientId).ToList();
        That(result, Has.Count.EqualTo(1));
        That(result[0].Content, Is.EqualTo("Hello"));
        That(result[0].SenderEmail, Is.EqualTo("sender@example.com"));
        That(result[0].RecipientEmail, Is.EqualTo("recipient@example.com"));
    }

    [Test]
    public void GetOutcomingMessagesByUserId_ReturnsMessagesWithUserEmails()
    {
        const int senderId = 1;
        const int recipientId = 2;
        var messageEntities = new List<MessageEntity>
        {
            new() { Id = 1, Content = "Hi", SenderId = senderId, RecipientId = recipientId }
        };
        var senderUserEntity = new UserEntity { Id = senderId, Email = "sender@example.com" };
        var recipientUserEntity = new UserEntity { Id = recipientId, Email = "recipient@example.com" };
        _messageRepositoryMock.Setup(repo => repo.FindBySenderId(senderId)).Returns(messageEntities);
        _userRepositoryMock.Setup(repo => repo.FindById(senderId)).Returns(senderUserEntity);
        _userRepositoryMock.Setup(repo => repo.FindById(recipientId)).Returns(recipientUserEntity);
        var result = _messageService.GetOutcomingMessagesByUserId(senderId).ToList();
        That(result, Has.Count.EqualTo(1));
        That(result[0].Content, Is.EqualTo("Hi"));
        That(result[0].SenderEmail, Is.EqualTo("sender@example.com"));
        That(result[0].RecipientEmail, Is.EqualTo("recipient@example.com"));
    }

    [Test]
    public void SendMessage_ThrowsArgumentNullException_WhenContentIsNullOrEmpty()
    {
        var messageSendingData = new MessageSendingData { Content = "", SenderId = 1, RecipientEmail = "recipient@example.com" };
        Throws<ArgumentNullException>(() => _messageService.SendMessage(messageSendingData));
    }

    [Test]
    public void SendMessage_ThrowsArgumentOutOfRangeException_WhenContentIsTooLong()
    {
        var longContent = new string('a', 5001);
        var messageSendingData = new MessageSendingData { Content = longContent, SenderId = 1, RecipientEmail = "recipient@example.com" };
        Throws<ArgumentOutOfRangeException>(() => _messageService.SendMessage(messageSendingData));
    }

    [Test]
    public void SendMessage_ThrowsUserNotFoundException_WhenRecipientNotFound()
    {
        var messageSendingData = new MessageSendingData { Content = "Hello", SenderId = 1, RecipientEmail = "nonexistent@example.com" };
        _userRepositoryMock.Setup(repo => repo.FindByEmail(messageSendingData.RecipientEmail)).Returns((UserEntity)null!);
        Throws<UserNotFoundException>(() => _messageService.SendMessage(messageSendingData));
    }

    [Test]
    public void SendMessage_ThrowsException_WhenMessageCreationFails()
    {
        var messageSendingData = new MessageSendingData { Content = "Hello", SenderId = 1, RecipientEmail = "recipient@example.com" };
        var recipientUserEntity = new UserEntity { Id = 2, Email = "recipient@example.com" };
        _userRepositoryMock.Setup(repo => repo.FindByEmail(messageSendingData.RecipientEmail)).Returns(recipientUserEntity);
        _messageRepositoryMock.Setup(repo => repo.Create(It.IsAny<MessageEntity>())).Returns(0);
        Throws<Exception>(() => _messageService.SendMessage(messageSendingData));
    }

    [Test]
    public void SendMessage_SuccessfullyCreatesMessage()
    {
        var messageSendingData = new MessageSendingData { Content = "Hello", SenderId = 1, RecipientEmail = "recipient@example.com" };
        var recipientUserEntity = new UserEntity { Id = 2, Email = "recipient@example.com" };
        _userRepositoryMock.Setup(repo => repo.FindByEmail(messageSendingData.RecipientEmail)).Returns(recipientUserEntity);
        _messageRepositoryMock.Setup(repo => repo.Create(It.IsAny<MessageEntity>())).Returns(1);
        _messageService.SendMessage(messageSendingData);
        _messageRepositoryMock.Verify(repo => repo.Create(It.IsAny<MessageEntity>()), Times.Once);
    }
}