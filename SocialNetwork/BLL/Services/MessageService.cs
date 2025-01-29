using SocialNetwork.DAL.Repositories.Interfaces;
using SocialNetwork.DAL.Repositories;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.DAL.Entities;

namespace SocialNetwork.BLL.Services;

public class MessageService
{
    readonly IMessageRepository _messageRepository = new MessageRepository();
    readonly IUserRepository _userRepository = new UserRepository();

    public IEnumerable<Message> GetIncomingMessagesByUserId(int recipientId)
    {
        var messages = new List<Message>();

        _messageRepository.FindByRecipientId(recipientId).ToList().ForEach(m =>
        {
            var senderUserEntity = _userRepository.FindById(m.SenderId);
            var recipientUserEntity = _userRepository.FindById(m.RecipientId);

            messages.Add(new Message(m.Id, m.Content, senderUserEntity?.Email, recipientUserEntity?.Email));
        });

        return messages;
    }

    public IEnumerable<Message> GetOutcomingMessagesByUserId(int senderId)
    {
        var messages = new List<Message>();

        _messageRepository.FindBySenderId(senderId).ToList().ForEach(m =>
        {
            var senderUserEntity = _userRepository.FindById(m.SenderId);
            var recipientUserEntity = _userRepository.FindById(m.RecipientId);

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

        var findUserEntity = _userRepository.FindByEmail(messageSendingData.RecipientEmail);
        if (findUserEntity is null) throw new UserNotFoundException(messageSendingData.RecipientEmail);

        var messageEntity = new MessageEntity()
        {
            Content = messageSendingData.Content,
            SenderId = messageSendingData.SenderId,
            RecipientId = findUserEntity.Id
        };

        if (_messageRepository.Create(messageEntity) == 0)
            throw new Exception();
    }
}