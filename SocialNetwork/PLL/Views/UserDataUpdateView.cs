using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Helpers;

namespace SocialNetwork.PLL.Views;

/// <summary>
/// Представляет представление для обновления данных пользователя.
/// </summary>
public class UserDataUpdateView
{
    private readonly UserService? _userService;

    /// <summary>
    /// Инициализирует экземпляр класса <see cref="UserDataUpdateView"/>.
    /// </summary>
    /// <param name="userService">Сервис для обновления данных пользователя.</param>
    public UserDataUpdateView(UserService? userService)
    {
        // Инициализация сервиса для работы с данными пользователя.
        _userService = userService;
    }

    /// <summary>
    /// Отображает интерфейс для обновления данных пользователя и обновляет профиль.
    /// </summary>
    /// <param name="user">Пользователь, чьи данные необходимо обновить.</param>
    public void Show(User? user)
    {
        // Запрос имени пользователя.
        Console.Write("Меня зовут:");
        user!.FirstName = Console.ReadLine();

        // Запрос фамилии пользователя.
        Console.Write("Моя фамилия:");
        user.LastName = Console.ReadLine();

        // Запрос ссылки на фото пользователя.
        Console.Write("Ссылка на моё фото:");
        user.Photo = Console.ReadLine();

        // Запрос любимого фильма пользователя.
        Console.Write("Мой любимый фильм:");
        user.FavoriteMovie = Console.ReadLine();

        // Запрос любимой книги пользователя.
        Console.Write("Моя любимая книга:");
        user.FavoriteBook = Console.ReadLine();

        // Обновление данных пользователя в сервисе.
        _userService?.Update(user);

        // Показ сообщения об успешном обновлении профиля.
        SuccessMessage.Show("Ваш профиль успешно обновлён!");
    }
}
