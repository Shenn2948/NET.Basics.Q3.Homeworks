using Task3.Models;

var employee1 = new Employee { Name = "Jhon Carter" };
var employee2 = new Employee { Name = "Luke Skywalker" };
var department = new Department { Name = "A", Employees = new List<Employee> { employee1, employee2 } };

Console.WriteLine("before clone:");
Console.WriteLine(department);

Department deepClone = department.DeepClone();

deepClone.Name = "Cloned name change";
deepClone.Employees[0].Name = "Bruce Willis";

Console.WriteLine("after deep clone:");
Console.WriteLine(department);

Department shallowClone = department.ShallowClone();

shallowClone.Name = "Cloned name change";
shallowClone.Employees[0].Name = "Bruce Willis";

Console.WriteLine("after shallow clone:");
Console.WriteLine(department);

Console.ReadLine();