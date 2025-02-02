using System.Data;
using System.Data.SQLite;
using Dapper;

namespace SocialNetwork.DAL.Repositories;

/// <summary>
/// Базовый репозиторий для работы с базой данных SQLite.
/// Предоставляет методы для выполнения SQL-запросов, таких как выборка и выполнение команд.
/// </summary>
public class BaseRepository
{
    /// <summary>
    /// Выполняет SQL-запрос и возвращает первый результат или значение по умолчанию, если результат не найден.
    /// </summary>
    /// <typeparam name="T">Тип данных, который ожидается в результате запроса.</typeparam>
    /// <param name="sql">SQL-запрос для выполнения.</param>
    /// <param name="parameters">Параметры запроса (необязательный параметр).</param>
    /// <returns>Первый найденный результат или значение по умолчанию, если запись не найдена.</returns>
    protected T? QueryFirstOrDefault<T>(string sql, object? parameters = null)
    {
        // Создание и открытие соединения с базой данных
        using var connection = CreateConnection();
        connection.Open();

        // Выполнение запроса и возврат первого результата или значения по умолчанию
        return connection.QueryFirstOrDefault<T>(sql, parameters);
    }

    /// <summary>
    /// Выполняет SQL-запрос и возвращает все результаты в виде списка.
    /// </summary>
    /// <typeparam name="T">Тип данных, который ожидается в результате запроса.</typeparam>
    /// <param name="sql">SQL-запрос для выполнения.</param>
    /// <param name="parameters">Параметры запроса (необязательный параметр).</param>
    /// <returns>Список результатов запроса.</returns>
    protected List<T> Query<T>(string sql, object? parameters = null)
    {
        // Создание и открытие соединения с базой данных
        using var connection = CreateConnection();
        connection.Open();

        // Выполнение запроса и возврат всех результатов в виде списка
        return connection.Query<T>(sql, parameters).ToList();
    }

    /// <summary>
    /// Выполняет SQL-команду, которая не возвращает результатов (например, INSERT, UPDATE, DELETE).
    /// </summary>
    /// <param name="sql">SQL-команда для выполнения.</param>
    /// <param name="parameters">Параметры команды (необязательный параметр).</param>
    /// <returns>Число строк, затронутых выполненной командой.</returns>
    /// <exception cref="Exception">Выбрасывается при ошибке выполнения команды.</exception>
    protected int Execute(string sql, object? parameters = null)
    {
        try
        {
            // Создание и открытие соединения с базой данных
            using var connection = CreateConnection();
            connection.Open();

            // Выполнение SQL-команды и возврат числа затронутых строк
            return connection.Execute(sql, parameters);
        }
        catch (Exception e)
        {
            // Логирование исключения в консоль
            Console.WriteLine(e);

            // Повторное выбрасывание исключения
            throw;
        }
    }

    /// <summary>
    /// Создает и возвращает новое соединение с базой данных SQLite.
    /// </summary>
    /// <returns>Соединение с базой данных SQLite.</returns>
    private IDbConnection CreateConnection()
    {
        // Возвращение нового соединения с указанной строкой подключения
        return new SQLiteConnection("Data Source = DAL/DB/finalprojectDB.db; Version = 3");
    }
}
