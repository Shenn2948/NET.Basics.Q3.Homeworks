using System.Xml.Serialization;

namespace Task1.XML.Models;

public class Employee
{
    [XmlAttribute]
    public string? Name { get; set; }
}
