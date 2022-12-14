using System.Runtime.Serialization;
using System.Text;

namespace Task2.Models;

[Serializable]
public class Person : ISerializable
{
    public string? Name { get; set; }
    public string? LastName { get; set; }

    public Person() { }

    public Person(SerializationInfo info, StreamingContext context)
    {
        Name = info.GetValue("name", typeof(string)) as string;
        LastName = info.GetValue("lastName", typeof(string)) as string;
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("name", Name);
        info.AddValue("lastName", LastName);
    }

    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.AppendLine($"Name: {Name}");
        sb.AppendLine($"lastName: {LastName}");
        sb.AppendLine();

        return sb.ToString();
    }
}
