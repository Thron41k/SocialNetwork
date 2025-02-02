using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Views;

namespace SocialNetwork;

internal abstract class Program
{
    // Сервисы
    private static MessageService? _messageService;  // Сервис для обработки сообщений.
    private static UserService? _userService;        // Сервис для работы с пользователями.

    // Представления
    private static MainView? _mainView;                              // Главное представление, отображающее интерфейс приложения.
    public static RegistrationView? RegistrationView;                // Представление для регистрации нового пользователя.
    public static AuthenticationView? AuthenticationView;            // Представление для аутентификации пользователя.
    public static UserMenuView? UserMenuView;                        // Представление меню пользователя.
    public static UserInfoView? UserInfoView;                        // Представление для отображения информации о пользователе.
    public static UserDataUpdateView? UserDataUpdateView;            // Представление для обновления данных пользователя.
    public static MessageSendingView? MessageSendingView;            // Представление для отправки сообщений.
    public static UserIncomingMessageView? UserIncomingMessageView;  // Представление для входящих сообщений пользователя.
    public static UserOutcomingMessageView? UserOutcomingMessageView; // Представление для исходящих сообщений пользователя.
    public static AddingFriendView? AddingFriendView;                // Представление для добавления друга.
    public static UserFriendView? UserFriendView;                    // Представление для отображения списка друзей пользователя.

    private static void Main()
    {
        // Инициализация сервисов.
        _userService = new UserService();  // Создание экземпляра сервиса пользователей.
        _messageService = new MessageService();  // Создание экземпляра сервиса сообщений.

        // Инициализация представлений и передача необходимых зависимостей.
        _mainView = new MainView();  // Главное представление приложения.
        RegistrationView = new RegistrationView(_userService);  // Представление для регистрации пользователей.
        AuthenticationView = new AuthenticationView(_userService);  // Представление для аутентификации пользователей.
        UserMenuView = new UserMenuView(_userService);  // Представление для меню пользователя.
        UserInfoView = new UserInfoView();  // Представление для отображения информации о пользователе.
        UserDataUpdateView = new UserDataUpdateView(_userService);  // Представление для обновления данных пользователя.
        MessageSendingView = new MessageSendingView(_messageService, _userService);  // Представление для отправки сообщений.
        UserIncomingMessageView = new UserIncomingMessageView();  // Представление для отображения входящих сообщений.
        UserOutcomingMessageView = new UserOutcomingMessageView();  // Представление для отображения исходящих сообщений.
        AddingFriendView = new AddingFriendView(_userService);  // Представление для добавления друзей.
        UserFriendView = new UserFriendView();  // Представление для отображения списка друзей.

        // Цикл, который постоянно отображает главное окно приложения.
        while (true)
        {
            _mainView.Show();  // Показывает главное окно.
        }
    }
}
