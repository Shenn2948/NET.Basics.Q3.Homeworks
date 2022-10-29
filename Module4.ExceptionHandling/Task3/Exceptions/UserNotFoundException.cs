using System;

namespace Task3.Exceptions;

[Serializable]
public sealed class UserNotFoundException : BaseUserTaskException
{
    public UserNotFoundException(string attributeKey, string message) : base(attributeKey, message) {}
}