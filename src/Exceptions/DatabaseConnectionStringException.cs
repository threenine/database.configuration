using System;

namespace Threenine.Exceptions;

public class DatabaseConnectionStringException(string message) : Exception(message);
