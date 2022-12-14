using MessagePack;

namespace Task1.Binary.Models;

[Serializable]
[MessagePackObject]
public class Employee
{
    [Key(0)]
    public string? Name { get; set; }
}
