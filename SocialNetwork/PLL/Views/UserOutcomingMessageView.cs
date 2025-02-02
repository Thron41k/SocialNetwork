using SocialNetwork.BLL.Models;

namespace SocialNetwork.PLL.Views;

/// <summary>
/// Класс, отвечающий за отображение исходящих сообщений.
/// </summary>
public class UserOutcomingMessageView
{
    /// <summary>
    /// Отображает список исходящих сообщений. Если сообщений нет, выводит соответствующее сообщение.
    /// </summary>
    /// <param name="outcomingMessages">Список исходящих сообщений, который нужно отобразить.</param>
    public void Show(IEnumerable<Message> outcomingMessages)
    {
        // Выводим заголовок для исходящих сообщений
        Console.WriteLine("Исходящие сообщения");

        // Преобразуем IEnumerable в List для удобства дальнейшей обработки
        var outcomingMessagesList = outcomingMessages.ToList();

        // Если список пуст, выводим сообщение об отсутствии исходящих сообщений
        if (!outcomingMessagesList.Any())
        {
            Console.WriteLine("Исходящих сообщений нет");
            return;
        }

        // Перебираем все сообщения в списке и выводим информацию о каждом сообщении
        outcomingMessagesList.ForEach(message =>
        {
            // Форматированный вывод информации о получателе и содержимом сообщения
            Console.WriteLine("Кому: {0}. Текст сообщения: {1}", message.RecipientEmail, message.Content);
        });
    }
}
