﻿using System.Data;
using System.Data.SQLite;
using Dapper;

namespace SocialNetwork.DAL.Repositories;

public class BaseRepository
{
    protected T? QueryFirstOrDefault<T>(string sql, object? parameters = null)
    {
        using var connection = CreateConnection();
        connection.Open();
        return connection.QueryFirstOrDefault<T>(sql, parameters);
    }

    protected List<T> Query<T>(string sql, object? parameters = null)
    {
        using var connection = CreateConnection();
        connection.Open();
        return connection.Query<T>(sql, parameters).ToList();
    }

    protected int Execute(string sql, object? parameters = null)
    {
        try
        {
            using var connection = CreateConnection();
            connection.Open();
            return connection.Execute(sql, parameters);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private IDbConnection CreateConnection()
    {
        return new SQLiteConnection("Data Source = DAL/DB/finalprojectDB.db; Version = 3");
    }
}