using MongoDB.Driver;
using StudentsApi.Model;

namespace StudentsApi.Data;

public class StudentDbContext
{
    private readonly IMongoDatabase _database;
    
    public StudentDbContext(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    public IMongoCollection<Student> Students => _database.GetCollection<Student>("Students");
}