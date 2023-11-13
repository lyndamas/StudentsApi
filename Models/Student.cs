using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StudentsApi.Model;

public class Student
{
    [BsonId]
    public string? Id { get; set; }

    [BsonElement("Name")]
    public string? Name { get; set; } 
}
