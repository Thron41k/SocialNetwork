using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Helpers;

namespace SocialNetwork.PLL.Views;

public class AuthenticationView(UserService? userService)
{
    public void Show()
    {
        var authenticationData = new UserAuthenticationData();

        Console.WriteLine("Введите почтовый адрес:");
        authenticationData.Email = Console.ReadLine();

        Console.WriteLine("Введите пароль:");
        authenticationData.Password = Console.ReadLine();

        try
        {
            var user = userService?.Authenticate(authenticationData);

            SuccessMessage.Show("Вы успешно вошли в социальную сеть!");
            SuccessMessage.Show("Добро пожаловать " + user?.FirstName);

            Program.UserMenuView?.Show(user);
        }

        catch (WrongPasswordException)
        {
            AlertMessage.Show("Пароль не корректный!");
        }

        catch (UserNotFoundException)
        {
            AlertMessage.Show("Пользователь не найден!");
        }

    }
}