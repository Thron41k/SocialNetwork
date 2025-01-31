namespace SocialNetwork.PLL.Helpers;

public static class SuccessMessage
{
    public static void Show(string message)
    {
        var originalColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ForegroundColor = originalColor;
    }
}