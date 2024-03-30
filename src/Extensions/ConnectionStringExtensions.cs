using System;
using System.Data;
using System.Data.Common;
using Threenine.Database.Exceptions;

namespace Threenine.Database.Extensions;

public static class ConnectionStringExtensions
{
   public static bool Validate(this string connectionString)
    {
        try
        {
            var factory = DbProviderFactories.GetFactory(connectionString);
            using var connection = factory.CreateConnection();
            if (connection == null)
                throw new DatabaseConfigurationException(ConfigurationErrors.DatabaseConnectionError);
      
            connection.Open();
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
                return true;  
            }
        }
        catch (Exception)
        {
            throw new DatabaseConfigurationException(ConfigurationErrors.DatabaseConnectionError);
        }
        return false;
    }
}