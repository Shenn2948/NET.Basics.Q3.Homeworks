using System.Text.Json.Serialization;

namespace Task1.JSON.Models;

public class Employee
{
    [JsonPropertyName("EmployeeName")]
    public string? Name { get; set; }
}
