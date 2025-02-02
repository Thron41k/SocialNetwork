using SocialNetwork.BLL.Models;

namespace SocialNetwork.PLL.Views;

/// <summary>
/// Класс отображает входящие сообщения для пользователя.
/// </summary>
public class UserIncomingMessageView
{
    /// <summary>
    /// Отображает список входящих сообщений.
    /// </summary>
    /// <param name="incomingMessages">Коллекция входящих сообщений.</param>
    public void Show(IEnumerable<Message> incomingMessages)
    {
        // Выводим заголовок для входящих сообщений
        Console.WriteLine("Входящие сообщения");

        // Преобразуем коллекцию входящих сообщений в список для удобства работы
        var incomingMessagesList = incomingMessages.ToList();

        // Проверяем, есть ли входящие сообщения
        if (!incomingMessagesList.Any())
        {
            // Если сообщений нет, выводим соответствующее сообщение
            Console.WriteLine("Входящих сообщения нет");
            return;
        }

        // Перебираем все входящие сообщения и выводим информацию о каждом
        incomingMessagesList.ForEach(message =>
        {
            // Для каждого сообщения выводим информацию об отправителе и содержимом
            Console.WriteLine("От кого: {0}. Текст сообщения: {1}", message.SenderEmail, message.Content);
        });
    }
}
