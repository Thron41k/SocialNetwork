using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories;
using SocialNetwork.DAL.Repositories.Interfaces;

namespace SocialNetwork.BLL.Services;

public class MessageService(IMessageRepository messageRepository, IUserRepository userRepository)
{
    public MessageService() : this(new MessageRepository(), new UserRepository())
    {
    }

    public IEnumerable<Message> GetIncomingMessagesByUserId(int recipientId)
    {
        var messages = new List<Message>();

        messageRepository.FindByRecipientId(recipientId).ToList().ForEach(m =>
        {
            var senderUserEntity = userRepository.FindById(m.SenderId);
            var recipientUserEntity = userRepository.FindById(m.RecipientId);

            messages.Add(new Message(m.Id, m.Content, senderUserEntity?.Email, recipientUserEntity?.Email));
        });

        return messages;
    }

    public IEnumerable<Message> GetOutcomingMessagesByUserId(int senderId)
    {
        var messages = new List<Message>();

        messageRepository.FindBySenderId(senderId).ToList().ForEach(m =>
        {
            var senderUserEntity = userRepository.FindById(m.SenderId);
            var recipientUserEntity = userRepository.FindById(m.RecipientId);

            messages.Add(new Message(m.Id, m.Content, senderUserEntity?.Email, recipientUserEntity?.Email));
        });

        return messages;
    }

    public void SendMessage(MessageSendingData messageSendingData)
    {
        if (string.IsNullOrEmpty(messageSendingData.Content))
            throw new ArgumentNullException();

        if (messageSendingData.Content.Length > 5000)
            throw new ArgumentOutOfRangeException();

        var findUserEntity = userRepository.FindByEmail(messageSendingData.RecipientEmail);
        if (findUserEntity is null) throw new UserNotFoundException();

        var messageEntity = new MessageEntity
        {
            Content = messageSendingData.Content,
            SenderId = messageSendingData.SenderId,
            RecipientId = findUserEntity.Id
        };

        if (messageRepository.Create(messageEntity) == 0)
            throw new Exception();
    }
}