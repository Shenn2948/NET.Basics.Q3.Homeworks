using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

using Task2.Models;

const string FileName = "person.bin";

var person = new Person { Name = "Jhon", LastName = "Carter" };

Console.WriteLine("before");
Console.WriteLine(person);

Serialize(person);
var deserialized = Deserialize();

Console.WriteLine("after");
Console.WriteLine(deserialized);

Console.ReadLine();

static void Serialize(Person person)
{
    using FileStream createStream = File.Create(FileName);
    IFormatter formatter = new BinaryFormatter();
#pragma warning disable SYSLIB0011 // Type or member is obsolete
    formatter.Serialize(createStream, person);
#pragma warning restore SYSLIB0011 // Type or member is obsolete
}

static Person? Deserialize()
{
    using FileStream openStream = File.OpenRead(FileName);
    IFormatter formatter = new BinaryFormatter();
#pragma warning disable SYSLIB0011 // Type or member is obsolete
    return formatter.Deserialize(openStream) as Person;
#pragma warning restore SYSLIB0011 // Type or member is obsolete
}