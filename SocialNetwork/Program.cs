using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Views;

namespace SocialNetwork;

internal abstract class Program
{
    private static MessageService? _messageService;
    private static UserService? _userService;
    private static MainView? _mainView;
    public static RegistrationView? RegistrationView;
    public static AuthenticationView? AuthenticationView;
    public static UserMenuView? UserMenuView;
    public static UserInfoView? UserInfoView;
    public static UserDataUpdateView? UserDataUpdateView;
    public static MessageSendingView? MessageSendingView;
    public static UserIncomingMessageView? UserIncomingMessageView;
    public static UserOutcomingMessageView? UserOutcomingMessageView;
    public static AddingFriendView? AddingFriendView;
    public static UserFriendView? UserFriendView;

    private static void Main()
    {
        _userService = new UserService();
        _messageService = new MessageService();

        _mainView = new MainView();
        RegistrationView = new RegistrationView(_userService);
        AuthenticationView = new AuthenticationView(_userService);
        UserMenuView = new UserMenuView(_userService);
        UserInfoView = new UserInfoView();
        UserDataUpdateView = new UserDataUpdateView(_userService);
        MessageSendingView = new MessageSendingView(_messageService, _userService);
        UserIncomingMessageView = new UserIncomingMessageView();
        UserOutcomingMessageView = new UserOutcomingMessageView();
        AddingFriendView = new AddingFriendView(_userService);
        UserFriendView = new UserFriendView();

        while (true)
        {
            _mainView.Show();
        }
    }
}