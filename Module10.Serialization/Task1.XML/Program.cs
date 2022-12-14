using System.Xml.Serialization;

using Task1.XML.Models;

const string FileName = "department.xml";
var employee1 = new Employee { Name = "Jhon Carter" };
var employee2 = new Employee { Name = "Luke Skywalker" };
var department = new Department { Name = "A", Employees = new List<Employee> { employee1, employee2 } };

Console.WriteLine("before");
Console.WriteLine(department);

Serialize(department);
Department? deserializedDep = Deserialize();

Console.WriteLine("after");
Console.WriteLine(deserializedDep);

Console.ReadLine();

static void Serialize(Department department)
{
    using FileStream createStream = File.Create(FileName);
    
    var serializer = new XmlSerializer(typeof(Department));
    serializer.Serialize(createStream, department);
}

static Department? Deserialize()
{
    using FileStream openStream = File.OpenRead(FileName);
    
    var serializer = new XmlSerializer(typeof(Department));
    return serializer.Deserialize(openStream) as Department;
}