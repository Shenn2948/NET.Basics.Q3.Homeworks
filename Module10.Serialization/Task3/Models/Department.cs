using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Task3.Models;

public class Department
{
    [JsonPropertyName("DepartmentName")]
    public string? Name { get; set; }

    public List<Employee> Employees { get; set; } = new();

    public Department DeepClone()
    {
        using var stream = new MemoryStream();
        JsonSerializer.Serialize(stream, this);
        stream.Position = 0;
        return JsonSerializer.Deserialize<Department>(stream)!;
    }

    public Department ShallowClone()
    {
        return (Department)this.MemberwiseClone();
    }

    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.AppendLine($"department: {Name}");
        sb.AppendLine("Employees: ");
        sb.AppendLine();

        for (int i = 0; i < Employees.Count; i++)
        {
            var empl = Employees[i];
            sb.AppendLine($"[{i}]: name={empl.Name}");
        }

        return sb.ToString();
    }
}
