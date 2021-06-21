using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using student_webapi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace student_webapi.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentApiContext _context;

        public StudentController(StudentApiContext context)
        {
            _context = context;
        }

        // GET: api/<StudentController>

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentItem>>> GetStudents()
        {
            
            return await _context.Students
                .OrderBy(s => s.Name).ToListAsync();
        }

        [HttpGet("withgradestats")]
        public async  Task<ActionResult<IEnumerable<StudentWrapper>>> GetStudentsWithGradeStats()
        {
            var students = await _context.Students.Select(s => new StudentWrapper
            {
                Id = s.Id,
                Name = s.Name,
                PhoneNumber = s.PhoneNumber,
                Born = s.Born,
                Year = s.Year,
                GradeCount = s.Grades.Count,
                FailCount = s.Grades.Count != 0 ? s.Grades.Where(g => g.Grade == 1).Count() : 0,
                GradeAverage = s.Grades.Count != 0 ? (float)s.Grades.Sum(g => g.Grade) / s.Grades.Count : 0,
                BestGrade = s.Grades.Count != 0 ?  s.Grades.Max(g => g.Grade) : 0
            }
            ).OrderByDescending(s => s.GradeAverage).ToListAsync();

            return students;
        }

        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentItem>> GetStudent(long id)
        {
            var studentItem = await _context.Students.FindAsync(id);

            if (studentItem == null)
            {
                return NotFound();
            }

            return studentItem;
        }

        // POST api/<StudentController>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<StudentItem>> PostStudent([FromBody] StudentItem studentItem)
        {
            
            _context.Students.Add(studentItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudent), new { id = studentItem.Id }, studentItem);
        }

        // PUT api/<StudentController>/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(long id, [FromBody] StudentItem studentItem)
        {
            if (id != studentItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(studentItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE api/<StudentController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<StudentItem>> DeleteStudent(long id)
        {
            var studentItem = await _context.Students.FindAsync(id);

            if (studentItem == null)
            {
                return NotFound();
            }
            else
            {
                _context.Students.Remove(studentItem);
                await _context.SaveChangesAsync();

                return studentItem;
            }

        }

        private bool StudentItemExists(long id)
        {
            return _context.Students.Any(s => s.Id == id);
        }
    }
}
