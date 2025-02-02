namespace SocialNetwork.PLL.Helpers;

/// <summary>
/// Класс для отображения успешных сообщений в консоли с зеленым цветом текста.
/// </summary>
public static class SuccessMessage
{
    /// <summary>
    /// Отображает сообщение об успехе в консоли, используя зеленый цвет текста.
    /// </summary>
    /// <param name="message">Сообщение, которое будет отображено в консоли.</param>
    public static void Show(string message)
    {
        // Сохраняем исходный цвет текста консоли
        var originalColor = Console.ForegroundColor;

        // Устанавливаем зеленый цвет для текста
        Console.ForegroundColor = ConsoleColor.Green;

        // Выводим сообщение в консоль
        Console.WriteLine(message);

        // Восстанавливаем исходный цвет текста консоли
        Console.ForegroundColor = originalColor;
    }
}
