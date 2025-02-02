using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Helpers;

namespace SocialNetwork.PLL.Views;

/// <summary>
/// Класс, отвечающий за добавление пользователя в список друзей.
/// </summary>
public class AddingFriendView
{
    private readonly UserService? _userService;

    /// <summary>
    /// Конструктор для инициализации класса с помощью сервиса пользователей.
    /// </summary>
    /// <param name="userService">Сервис для работы с пользователями.</param>
    public AddingFriendView(UserService? userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Метод для отображения интерфейса добавления пользователя в друзья.
    /// Запрашивает почтовый адрес пользователя и добавляет его в друзья текущему пользователю.
    /// </summary>
    /// <param name="user">Текущий пользователь, который пытается добавить друга.</param>
    public void Show(User? user)
    {
        try
        {
            // Проверка на null для переданного пользователя. Если null, выбрасывается исключение.
            if (user == null) throw new ArgumentNullException(nameof(user));

            // Создание объекта данных для добавления друга.
            var userAddingFriendData = new UserAddingFriendData();

            // Запрос почтового адреса пользователя для добавления в друзья.
            Console.WriteLine("Введите почтовый адрес пользователя которого хотите добавить в друзья: ");

            // Считывание почтового адреса.
            userAddingFriendData.FriendEmail = Console.ReadLine();

            // Идентификатор текущего пользователя.
            userAddingFriendData.UserId = user.Id;

            // Вызов метода для добавления пользователя в друзья.
            _userService?.AddFriend(userAddingFriendData);

            // Показ сообщения об успешном добавлении.
            SuccessMessage.Show("Вы успешно добавили пользователя в друзья!");
        }
        catch (UserNotFoundException)
        {
            // Обработка случая, когда пользователь с указанным почтовым адресом не найден.
            AlertMessage.Show("Пользователя с указанным почтовым адресом не существует!");
        }
        catch (Exception)
        {
            // Обработка всех остальных ошибок.
            AlertMessage.Show("Произошла ошибка при добавлении пользователя в друзья!");
        }
    }
}

