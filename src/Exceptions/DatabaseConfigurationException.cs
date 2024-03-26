using System;

namespace Threenine.Database.Exceptions;

public class DatabaseConfigurationException(string message) : Exception(message);
