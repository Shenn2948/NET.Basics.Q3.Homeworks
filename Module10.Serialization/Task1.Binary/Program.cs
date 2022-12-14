using MessagePack;

using Task1.Binary.Models;

var employee1 = new Employee { Name = "Jhon Carter" };
var employee2 = new Employee { Name = "Luke Skywalker" };
var department = new Department { Name = "A", Employees = new List<Employee> { employee1, employee2 } };

Console.WriteLine("before");
Console.WriteLine(department);

Serialize(department);
var deserializedDep = Deserialize();

Console.WriteLine("after");
Console.WriteLine(deserializedDep);

Console.ReadLine();

static void Serialize(Department department)
{
    Stream stream = new FileStream("Department.bin", FileMode.Create, FileAccess.Write, FileShare.None);
    MessagePackSerializer.Serialize(stream, department);

    stream.Close();
}

static Department Deserialize()
{
    Stream stream = new FileStream("Department.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
    Department obj = MessagePackSerializer.Deserialize<Department>(stream);
    stream.Close();

    return obj;
}

#if !NET5_0_OR_GREATER
Serialize(department);
var obj = Deserialize();

static void Serialize(Department department)
{
    IFormatter formatter = new BinaryFormatter();
    Stream stream = new FileStream("Department.bin", FileMode.Create, FileAccess.Write, FileShare.None);
    formatter.Serialize(stream, department);  // obsolete and should not be used
    stream.Close();
}

static Department Deserialize()
{
    IFormatter formatter = new BinaryFormatter();
    Stream stream = new FileStream("Department.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
    var obj = (Department)formatter.Deserialize(stream);
    stream.Close();

    return obj;
}
#endif