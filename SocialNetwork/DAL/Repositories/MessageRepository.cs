using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories.Interfaces;

namespace SocialNetwork.DAL.Repositories;

public class MessageRepository : BaseRepository, IMessageRepository
{
    public int Create(MessageEntity messageEntity)
    {
        return Execute(@"insert into messages(content, senderId, recipientId) 
                             values(:Content,:SenderId,:RecipientId)", messageEntity);
    }

    public IEnumerable<MessageEntity> FindBySenderId(int senderId)
    {
        return Query<MessageEntity>("select * from messages where senderId = :sender_id", new { sender_id = senderId });
    }

    public IEnumerable<MessageEntity> FindByRecipientId(int recipientId)
    {
        return Query<MessageEntity>("select * from messages where recipientId = :recipient_id", new { recipient_id = recipientId });
    }

    public int DeleteById(int messageId)
    {
        return Execute("delete from messages where id = :id", new { id = messageId });
    }

}