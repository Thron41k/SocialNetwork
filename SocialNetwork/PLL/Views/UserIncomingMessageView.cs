using SocialNetwork.BLL.Models;

namespace SocialNetwork.PLL.Views;

public class UserIncomingMessageView
{
    public void Show(IEnumerable<Message> incomingMessages)
    {
        Console.WriteLine("Входящие сообщения");

        var incomingMessagesList = incomingMessages.ToList();
        if (!incomingMessagesList.Any())
        {
            Console.WriteLine("Входящих сообщения нет");
            return;
        }

        incomingMessagesList.ForEach(message =>
        {
            Console.WriteLine("От кого: {0}. Текст сообщения: {1}", message.SenderEmail, message.Content);
        });
    }
}