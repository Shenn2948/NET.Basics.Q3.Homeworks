using System;

namespace Task3.Exceptions;

[Serializable]
public sealed class InvalidUserException : BaseUserTaskException
{
    public InvalidUserException(string attributeKey, string message) : base(attributeKey, message) {}
}