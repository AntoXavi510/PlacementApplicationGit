using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlacementApplicationNew.Model;
using PlacementApplicationNew.Repository;
using PlacementApplicationNew.Token;

namespace PlacementApplicationNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudent placement;

        public StudentsController(IStudent _placement)
        {
            placement = _placement;
        }

        // GET: api/Students
        /// <summary>
        /// List of the Students.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await placement.GetStudents();
        }
        // GET: api/Students/5
        /// <summary>
        /// Shows a Specific Student.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            //if (placement.GetStudent(id) == null)
            //{
            //    return NotFound();
            //}
            //  var student = await placement.GetStudent(id);

            //  if (student == null)
            //  {
            //      return NotFound();
            //  }

            return await placement.GetStudent(id);
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update a Specific Student.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
             await placement.UpdateStudent(student);
            return NoContent();
        }

        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754       
        /// <summary>
        /// Creates a Student.
        /// </summary>
        /// <response code="200">Returns the newly created item</response>
        /// <response code="400">If the student is already present or the values are not given properly</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            if (await placement.AddNewStudent(student) == null)
            {
                return BadRequest();
            }
            else { return student; }
        }
        /// <summary>
        /// Login as a Student.
        /// </summary>
        [HttpPost("Login")]
        public async Task<ActionResult<StudentToken>> Login(Student student)
        {

            if (await placement.Login(student) == null)
            { return BadRequest(); }
            else { return await placement.Login(student); }
        }

        // DELETE: api/Students/5
        /// <summary>
        /// Deletes a specific Student.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
          placement.DeleteStudent(id);
            return NoContent();
        }

        //    [HttpGet("{id}")]
        //    public async Task<IActionResult> GetStudent(int id)
        //    {

        //    }
        //}
    }
}
