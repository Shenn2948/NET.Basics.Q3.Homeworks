using System.Text.Json;
using System.Text.Json.Serialization;

namespace Task3.Models;

public class Employee
{
    [JsonPropertyName("EmployeeName")]
    public string? Name { get; set; }

    public Employee DeepCopy()
    {
        using var stream = new MemoryStream();
        JsonSerializer.Serialize(stream, this);
        stream.Position = 0;
        return JsonSerializer.Deserialize<Employee>(stream)!;
    }
}
