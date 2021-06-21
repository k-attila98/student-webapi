using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using student_webapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace student_webapi.Controllers
{
    [Route("api/grades")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly StudentApiContext _context;

        public GradeController(StudentApiContext context)
        {
            _context = context;
        }

        // GET: api/<GradeController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GradeItem>>> GetGrades()
        {
            return await _context.Grades.ToListAsync();
        }

        // GET api/<GradeController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GradeItem>> GetGrade(int id)
        {
            var gradeItem = await _context.Grades.FindAsync(id);

            if (gradeItem == null)
            {
                return NotFound();
            }

            return gradeItem;
        }

        // POST api/<GradeController>
        [Authorize]
        [HttpPost("student/{id}")]
        public async Task<ActionResult<GradeItem>> PostGrade(long id, [FromBody] GradeItem gradeItem)
        {
            gradeItem.StudentId = id;
            _context.Grades.Add(gradeItem);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGrade), new { id = gradeItem.Id }, gradeItem);
        }

        // POST api/<GradeController>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<GradeItem>> PostGradeA([FromBody] GradeItem gradeItem)
        {
            _context.Grades.Add(gradeItem);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGrade), new { id = gradeItem.Id }, gradeItem);
        }

        // PUT api/<GradeController>/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGrade(int id, [FromBody] GradeItem gradeItem)
        {
            if (id != gradeItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(gradeItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GradeItemExists(id))
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

        // DELETE api/<GradeController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<GradeItem>> DeleteGrade(long id)
        {
            var gradeItem = await _context.Grades.FindAsync(id);

            if (gradeItem == null)
            {
                return NotFound();
            }
            else 
            {
                _context.Grades.Remove(gradeItem);
                await _context.SaveChangesAsync();

                return gradeItem;
            }
        }

        [HttpGet("student/{id}")]
        public async Task<ActionResult<IEnumerable<GradeItem>>> GetStudentsGrades(long id)
        {
            return await _context.Grades.Include(g => g.Student).Where(g => g.StudentId == id).ToListAsync();
        }

        private bool GradeItemExists(long id)
        {
            return _context.Grades.Any(g => g.Id == id);
        }
    }
}
