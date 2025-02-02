using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;

namespace SocialNetwork.PLL.Views;

/// <summary>
/// Представление главного меню пользователя, в котором он может выбирать действия
/// с его профилем, друзьями и сообщениями.
/// </summary>
public class UserMenuView
{
    private readonly UserService? _userService;

    /// <summary>
    /// Конструктор для инициализации представления меню пользователя с возможностью
    /// работы с сервисом пользователя.
    /// </summary>
    /// <param name="userService">Сервис для работы с данными пользователей.</param>
    public UserMenuView(UserService? userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Отображает меню и позволяет пользователю выбрать доступное действие.
    /// После выбора действия программа будет выполнять соответствующие функции.
    /// </summary>
    /// <param name="user">Пользователь, чье меню отображается.</param>
    public void Show(User? user)
    {
        // Бесконечный цикл для отображения меню, пока пользователь не решит выйти
        while (true)
        {
            // Отображаем статистику пользователя (входящие/исходящие сообщения, друзья)
            Console.WriteLine("Входящие сообщения: {0}", user!.IncomingMessages.Count());
            Console.WriteLine("Исходящие сообщения: {0}", user.OutgoingMessages.Count());
            Console.WriteLine("Мои друзья: {0}", user.Friends.Count());

            // Отображаем доступные действия в меню
            Console.WriteLine("Просмотреть информацию о моём профиле (нажмите 1)");
            Console.WriteLine("Редактировать мой профиль (нажмите 2)");
            Console.WriteLine("Добавить в друзья (нажмите 3)");
            Console.WriteLine("Написать сообщение (нажмите 4)");
            Console.WriteLine("Просмотреть входящие сообщения (нажмите 5)");
            Console.WriteLine("Просмотреть исходящие сообщения (нажмите 6)");
            Console.WriteLine("Просмотреть моих друзей (нажмите 7)");
            Console.WriteLine("Выйти из профиля (нажмите 8)");

            // Считываем выбор пользователя
            var keyValue = Console.ReadLine();

            // Если пользователь выбрал "8", выходим из меню
            if (keyValue == "8") break;

            // В зависимости от выбора, выполняем нужное действие
            switch (keyValue)
            {
                case "1":
                    {
                        // Просмотр информации о профиле
                        Program.UserInfoView?.Show(user);
                        break;
                    }
                case "2":
                    {
                        // Редактирование профиля
                        Program.UserDataUpdateView?.Show(user);
                        // Обновляем информацию о пользователе после редактирования
                        user = _userService?.FindById(user.Id);
                        break;
                    }

                case "3":
                    {
                        // Добавление друга
                        Program.AddingFriendView?.Show(user);
                        // Обновляем информацию о пользователе после добавления друга
                        user = _userService?.FindById(user.Id);
                        break;
                    }

                case "4":
                    {
                        // Написание сообщения
                        Program.MessageSendingView?.Show(user);
                        // Обновляем информацию о пользователе после отправки сообщения
                        user = _userService?.FindById(user.Id);
                        break;
                    }

                case "5":
                    {
                        // Просмотр входящих сообщений
                        Program.UserIncomingMessageView?.Show(user.IncomingMessages);
                        break;
                    }

                case "6":
                    {
                        // Просмотр исходящих сообщений
                        Program.UserOutcomingMessageView?.Show(user.OutgoingMessages);
                        break;
                    }

                case "7":
                    {
                        // Просмотр друзей
                        Program.UserFriendView?.Show(user.Friends);
                        break;
                    }
            }
        }
    }
}
