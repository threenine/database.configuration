using System;
using System.Data.Common;
using Threenine.Database;
using Threenine.Database.Exceptions;
using Threenine.Exceptions;

namespace Threenine.Validators;

public class ConnectionsStrings
{
  public bool Validate(string connectionString)
  {
    try
    {
      var factory = DbProviderFactories.GetFactory(connectionString);
      using var connection = factory.CreateConnection();
      if (connection == null)
        throw new DatabaseConfigurationException(ConfigurationErrors.DatabaseConnectionError);
      
      connection.Open();
      return true;
    }
    catch (Exception e)
    {
      throw new DatabaseConfigurationException(ConfigurationErrors.DatabaseConnectionError);
    }
  }
}