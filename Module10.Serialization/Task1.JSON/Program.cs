using System.Text.Json;

using Task1.JSON.Models;

const string FileName = "department.json";
var employee1 = new Employee { Name = "Jhon Carter" };
var employee2 = new Employee { Name = "Luke Skywalker" };
var department = new Department { Name = "A", Employees = new List<Employee> { employee1, employee2 } };

Console.WriteLine("before");
Console.WriteLine(department);

await SerializeAsync(department);
var deserializedDep = await Deserialize();

Console.WriteLine("after");
Console.WriteLine(deserializedDep);

Console.ReadLine();

static async Task SerializeAsync(Department department)
{
    using FileStream createStream = File.Create(FileName);
    await JsonSerializer.SerializeAsync(createStream, department);
    await createStream.DisposeAsync();
}

static ValueTask<Department?> Deserialize()
{
    using FileStream openStream = File.OpenRead(FileName);
    return JsonSerializer.DeserializeAsync<Department>(openStream);
}