using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentsApi.Model;
using StudentsApi.Repository;
using MongoDB.Bson;

namespace StudentsApi.Controllers;

[ApiController]
[Route("students")]
public class StudentController : ControllerBase
{
    private readonly StudentRepository _repository;

    public StudentController(StudentRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
    {
        return Ok(await _repository.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Student>> GetStudent(string id)
    {
        var student = await _repository.GetByIdAsync(id);
        if (student == null)
        {
            return NotFound();
        }
        return Ok(student);
    }

    [HttpPost]
    public async Task<ActionResult<Student>> CreateStudent(Student student)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        await _repository.CreateAsync(student);
        return Ok(student);
    }

    [HttpPut]
    public async Task<IActionResult> Update(string id, Student student)
    {
        await _repository.UpdateAsync(id, student);
        return Ok(student?.Id);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var student = await _repository.GetByIdAsync(id);
        await _repository.DeleteAsync(student?.Id);
        return Ok(student?.Id);
    }
}
