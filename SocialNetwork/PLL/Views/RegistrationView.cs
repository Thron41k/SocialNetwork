using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Helpers;

namespace SocialNetwork.PLL.Views;

/// <summary>
/// Класс, который отображает интерфейс регистрации нового пользователя.
/// </summary>
public class RegistrationView
{
    private readonly UserService? _userService;

    /// <summary>
    /// Конструктор класса RegistrationView.
    /// </summary>
    /// <param name="userService">Сервис для обработки регистрации пользователей.</param>
    public RegistrationView(UserService? userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Метод для отображения процесса регистрации нового пользователя.
    /// Пользователю предлагается ввести личные данные для создания нового профиля.
    /// </summary>
    public void Show()
    {
        // Создаем объект для хранения данных о пользователе
        var userRegistrationData = new UserRegistrationData();

        // Запрос имени пользователя
        Console.WriteLine("Для создания нового профиля введите ваше имя:");
        userRegistrationData.FirstName = Console.ReadLine();

        // Запрос фамилии пользователя
        Console.Write("Ваша фамилия:");
        userRegistrationData.LastName = Console.ReadLine();

        // Запрос пароля пользователя
        Console.Write("Пароль:");
        userRegistrationData.Password = Console.ReadLine();

        // Запрос почтового адреса пользователя
        Console.Write("Почтовый адрес:");
        userRegistrationData.Email = Console.ReadLine();

        try
        {
            // Попытка регистрации пользователя через сервис
            _userService?.Register(userRegistrationData);

            // Если регистрация прошла успешно, выводим сообщение о успешном создании профиля
            SuccessMessage.Show("Ваш профиль успешно создан. Теперь Вы можете войти в систему под своими учетными данными.");
        }
        catch (ArgumentNullException)
        {
            // Если пользователь не ввел обязательное значение, выводим сообщение об ошибке
            AlertMessage.Show("Введите корректное значение.");
        }
        catch (Exception e)
        {
            // В случае других ошибок при регистрации, выводим сообщение с деталями ошибки
            AlertMessage.Show($"Произошла ошибка при регистрации.{e.Message}");
        }
    }
}
