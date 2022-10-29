using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Task3.Exceptions;

[Serializable]
public abstract class BaseUserTaskException : Exception
{
    protected BaseUserTaskException(string attributeKey, string message)
    {
        this.ResponseAttributes.Add(attributeKey, message);
    }

    protected BaseUserTaskException() {}

    protected BaseUserTaskException(string message) : base(message) {}

    protected BaseUserTaskException(string message, Exception inner) : base(message, inner) {}

    protected BaseUserTaskException(SerializationInfo info, StreamingContext context) : base(info, context) {}

    public Dictionary<string, string> ResponseAttributes { get; } = new();
}