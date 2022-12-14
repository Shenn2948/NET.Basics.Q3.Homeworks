using System.Text;

namespace Task1.JSON.Models;

public class Department
{
    public string? Name { get; set; }

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
