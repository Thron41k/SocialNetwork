namespace SocialNetwork.PLL.Helpers;

/// <summary>
/// Класс для отображения сообщений об ошибках в консоли с красным цветом текста.
/// </summary>
public static class AlertMessage
{
    /// <summary>
    /// Выводит сообщение в консоль с красным цветом текста, чтобы выделить его как предупреждение или ошибку.
    /// После отображения сообщения восстанавливает исходный цвет текста.
    /// </summary>
    /// <param name="message">Сообщение, которое нужно вывести в консоль.</param>
    public static void Show(string message)
    {
        // Сохраняем оригинальный цвет текста консоли, чтобы вернуть его после вывода сообщения
        var originalColor = Console.ForegroundColor;

        // Устанавливаем красный цвет для вывода сообщения об ошибке или предупреждении
        Console.ForegroundColor = ConsoleColor.Red;

        // Выводим сообщение в консоль
        Console.WriteLine(message);

        // Восстанавливаем исходный цвет текста консоли
        Console.ForegroundColor = originalColor;
    }
}
