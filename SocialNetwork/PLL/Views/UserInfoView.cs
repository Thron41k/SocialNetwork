using SocialNetwork.BLL.Models;

namespace SocialNetwork.PLL.Views;

/// <summary>
/// Класс, который отвечает за отображение информации о пользователе.
/// </summary>
public class UserInfoView
{
    /// <summary>
    /// Метод, который отображает информацию о пользователе в консоли.
    /// </summary>
    /// <param name="user">Объект типа User, содержащий информацию о пользователе.</param>
    public void Show(User? user)
    {
        // Проверка, что пользователь не равен null.
        if (user == null)
        {
            Console.WriteLine("Пользователь не найден.");
            return;
        }

        // Отображение информации о пользователе.
        Console.WriteLine("Информация о моем профиле");
        Console.WriteLine("Мой идентификатор: {0}", user.Id);
        Console.WriteLine("Меня зовут: {0}", user.FirstName);
        Console.WriteLine("Моя фамилия: {0}", user.LastName);
        Console.WriteLine("Мой пароль: {0}", user.Password);
        Console.WriteLine("Мой почтовый адрес: {0}", user.Email);
        Console.WriteLine("Ссылка на моё фото: {0}", user.Photo);
        Console.WriteLine("Мой любимый фильм: {0}", user.FavoriteMovie);
        Console.WriteLine("Моя любимая книга: {0}", user.FavoriteBook);
    }
}
