using SocialNetwork.BLL.Models;

namespace SocialNetwork.PLL.Views;

public class UserOutcomingMessageView
{
    public void Show(IEnumerable<Message> outcomingMessages)
    {
        Console.WriteLine("Исходящие сообщения");

        var outcomingMessagesList = outcomingMessages.ToList();
        if (!outcomingMessagesList.Any())
        {
            Console.WriteLine("Исходящих сообщений нет");
            return;
        }

        outcomingMessagesList.ForEach(message =>
        {
            Console.WriteLine("Кому: {0}. Текст сообщения: {1}", message.RecipientEmail, message.Content);
        });
    }
}