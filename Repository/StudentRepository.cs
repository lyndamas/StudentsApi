using MongoDB.Driver;
using StudentsApi.Data;
using StudentsApi.Model;

namespace StudentsApi.Repository
{
    public class StudentRepository
    {
        private readonly IMongoCollection<Student> _collection;

        public StudentRepository(StudentDbContext context)
        {
            _collection = context.Students;
        }

        public Task<List<Student>> GetAllAsync()
        {
            return _collection.Find(c => true).ToListAsync();
        }

        public Task<Student> GetByIdAsync(string id)
        {
            return _collection.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Student> CreateAsync(Student Student)
        {
            await _collection.InsertOneAsync(Student).ConfigureAwait(false);
            return Student;
        }

        public Task UpdateAsync(string id, Student Student)
        {
            return _collection.ReplaceOneAsync(c => c.Id == id, Student);
        }
        
        public Task DeleteAsync(string id)
        {
            return _collection.DeleteOneAsync(c => c.Id == id);
        }
    }
}