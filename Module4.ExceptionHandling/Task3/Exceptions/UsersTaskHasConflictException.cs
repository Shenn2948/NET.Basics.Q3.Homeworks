using System;

namespace Task3.Exceptions;

[Serializable]
public sealed class UsersTaskHasConflictException : BaseUserTaskException
{
    public UsersTaskHasConflictException(string attributeKey, string message) : base(attributeKey, message) {}
}