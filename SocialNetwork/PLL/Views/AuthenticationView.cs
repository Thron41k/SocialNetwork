using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Helpers;

namespace SocialNetwork.PLL.Views;

/// <summary>
/// Класс, который отвечает за отображение экрана аутентификации пользователя.
/// </summary>
public class AuthenticationView
{
    // Сервис для работы с пользователями.
    private readonly UserService? _userService;

    /// <summary>
    /// Конструктор для инициализации AuthenticationView.
    /// </summary>
    /// <param name="userService">Сервис для аутентификации и работы с пользователями.</param>
    public AuthenticationView(UserService? userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Отображает экран аутентификации, запрашивает у пользователя почтовый адрес и пароль,
    /// выполняет аутентификацию, и, в зависимости от результата, выводит соответствующие сообщения.
    /// </summary>
    public void Show()
    {
        // Создаем объект для хранения данных для аутентификации.
        var authenticationData = new UserAuthenticationData();

        // Запрашиваем у пользователя ввод почтового адреса.
        Console.WriteLine("Введите почтовый адрес:");
        authenticationData.Email = Console.ReadLine();

        // Запрашиваем у пользователя ввод пароля.
        Console.WriteLine("Введите пароль:");
        authenticationData.Password = Console.ReadLine();

        try
        {
            // Попытка аутентифицировать пользователя с помощью предоставленных данных.
            var user = _userService?.Authenticate(authenticationData);

            // Если аутентификация успешна, показываем успешное сообщение.
            SuccessMessage.Show("Вы успешно вошли в социальную сеть!");
            SuccessMessage.Show("Добро пожаловать " + user?.FirstName);

            // После успешного входа отображаем главное меню пользователя.
            Program.UserMenuView?.Show(user);
        }
        catch (WrongPasswordException)
        {
            // Если введен неверный пароль, показываем сообщение об ошибке.
            AlertMessage.Show("Пароль не корректный!");
        }
        catch (UserNotFoundException)
        {
            // Если пользователь не найден, показываем соответствующее сообщение.
            AlertMessage.Show("Пользователь не найден!");
        }
    }
}
