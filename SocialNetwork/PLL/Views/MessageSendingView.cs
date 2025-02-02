using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Helpers;

namespace SocialNetwork.PLL.Views;

/// <summary>
/// Представляет класс для отправки сообщений.
/// </summary>
public class MessageSendingView
{
    private readonly MessageService? _messageService;
    private readonly UserService? _userService;

    /// <summary>
    /// Конструктор класса MessageSendingView.
    /// </summary>
    /// <param name="messageService">Сервис для отправки сообщений.</param>
    /// <param name="userService">Сервис для работы с пользователями.</param>
    public MessageSendingView(MessageService? messageService, UserService? userService)
    {
        _messageService = messageService;
        _userService = userService;
    }

    /// <summary>
    /// Метод для отображения процесса отправки сообщения пользователем.
    /// </summary>
    /// <param name="user">Пользователь, отправляющий сообщение.</param>
    public void Show(User? user)
    {

        // Создаем объект для хранения данных о сообщении
        var messageSendingData = new MessageSendingData();

        // Запрашиваем почтовый адрес получателя
        Console.Write("Введите почтовый адрес получателя: ");
        messageSendingData.RecipientEmail = Console.ReadLine();

        // Запрашиваем содержимое сообщения
        Console.WriteLine("Введите сообщение (не больше 5000 символов): ");
        messageSendingData.Content = Console.ReadLine();

        // Устанавливаем ID отправителя (пользователь)
        messageSendingData.SenderId = user!.Id;

        try
        {
            // Отправляем сообщение через сервис MessageService
            _messageService?.SendMessage(messageSendingData);

            // Показать успешное сообщение
            SuccessMessage.Show("Сообщение успешно отправлено!");

            // Обновляем информацию о пользователе
            user = _userService?.FindById(user.Id);
        }
        catch (UserNotFoundException)
        {
            // Обработка ошибки: пользователь не найден
            AlertMessage.Show("Пользователь не найден!");
        }
        catch (ArgumentNullException)
        {
            // Обработка ошибки: введено некорректное значение
            AlertMessage.Show("Введите корректное значение!");
        }
        catch (Exception)
        {
            // Обработка непредвиденной ошибки при отправке сообщения
            AlertMessage.Show("Произошла ошибка при отправке сообщения!");
        }
    }
}
