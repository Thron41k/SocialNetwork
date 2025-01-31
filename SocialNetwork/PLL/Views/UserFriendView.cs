using SocialNetwork.BLL.Models;

namespace SocialNetwork.PLL.Views;

public class UserFriendView
{
    public void Show(IEnumerable<User?> friends)
    {
        Console.WriteLine("Мои друзья");

        var friendsList = friends.ToList();
        if (!friendsList.Any())
        {
            Console.WriteLine("У вас нет друзей");
            return;
        }

        friendsList.ForEach(friend =>
        {
            Console.WriteLine("Почтовый адрес друга: {0}. Имя друга: {1}. Фамилия друга: {2}", friend?.Email, friend?.FirstName, friend?.LastName);
        });

    }

}