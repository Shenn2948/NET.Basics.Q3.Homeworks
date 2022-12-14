using System.Text;

using MessagePack;

namespace Task1.Binary.Models;

[Serializable]
[MessagePackObject]
public class Department
{
    [Key(0)]
    public string? Name { get; set; }

    [Key(1)]
    public List<Employee>? Employees { get; set; }

    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.AppendLine($"department: {Name}");
        sb.AppendLine("Employees: ");
        sb.AppendLine();

        if (Employees == null)
        {
            return sb.ToString();
        }

        for (int i = 0; i < Employees.Count; i++)
        {
            var empl = Employees[i];
            sb.AppendLine($"[{i}]: name={empl.Name}");
        }

        sb.AppendLine();

        return sb.ToString();
    }
}
