using SocialNetwork.BLL.Models;

namespace SocialNetwork.PLL.Views;

/// <summary>
/// Класс, отвечающий за отображение списка друзей пользователя.
/// </summary>
public class UserFriendView
{
    /// <summary>
    /// Отображает список друзей пользователя.
    /// </summary>
    /// <param name="friends">Коллекция объектов типа <see cref="User"/>, представляющая друзей пользователя.</param>
    public void Show(IEnumerable<User?> friends)
    {
        // Выводим заголовок
        Console.WriteLine("Мои друзья");

        // Преобразуем коллекцию в список для удобства работы
        var friendsList = friends.ToList();

        // Проверяем, есть ли друзья в списке
        if (!friendsList.Any())
        {
            // Если список пуст, выводим сообщение об отсутствии друзей
            Console.WriteLine("У вас нет друзей");
            return;
        }

        // Для каждого друга из списка выводим его данные
        friendsList.ForEach(friend =>
        {
            // Выводим почтовый адрес, имя и фамилию друга.
            Console.WriteLine("Почтовый адрес друга: {0}. Имя друга: {1}. Фамилия друга: {2}",
                friend?.Email, friend?.FirstName, friend?.LastName);
        });
    }
}
