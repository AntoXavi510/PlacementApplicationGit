using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlacementApplicationNew.Model;
using PlacementApplicationNew.Repository;

namespace PlacementApplicationNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppliesController : ControllerBase
    {
        private readonly IApply _context;
        public AppliesController(IApply context)
        {
            _context = context;
        }

        // GET: api/Applies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Apply>>> GetApplys()
        {
            //if (_context.Applys == null)
            //{
            //    return NotFound();
            //}
            //  return await _context.Applys.ToListAsync();
            return await _context.GetApplys();
        }
        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Apply>>> GetRolesForStudent(int id)
        {
            //if (_context.Applys == null)
            //{
            //    return NotFound();
            //}
            //  return await _context.Applys.ToListAsync();
            return await _context.GetRolesForStudent(id);
        }
        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Apply>>> GetApplyForRoles(int id)
        {
            //if (_context.Applys == null)
            //{
            //    return NotFound();
            //}
            //  return await _context.Applys.ToListAsync();
            return await _context.GetApplyForRoles(id);
        }
        // GET: api/Applies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Apply>> GetApply(int id)
        {
            //if (_context.Applys == null)
            //{
            //    return NotFound();
            //}
            //  var apply = await _context.Applys.FindAsync(id);

            //  if (apply == null)
            //  {
            //      return NotFound();
            //  }
            //  return apply;
          
            return await _context.GetApply(id);
        }

        //// PUT: api/Applies/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutApply(int id, Apply apply)
        //{
        //    //if (id != apply.Id)
        //    //{
        //    //    return BadRequest();
        //    //}

        //    //_context.Entry(apply).State = EntityState.Modified;

        //    //try
        //    //{
        //    //    await _context.SaveChangesAsync();
        //    //}
        //    //catch (DbUpdateConcurrencyException)
        //    //{
        //    //    if (!ApplyExists(id))
        //    //    {
        //    //        return NotFound();
        //    //    }
        //    //    else
        //    //    {
        //    //        throw;
        //    //    }
        //    //}
        //    await _context.UpdateStudent(student);
        //    return NoContent();
        //}

        // POST: api/Applies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Apply>> PostApply(Apply apply)
        {
            //if (_context.Applys == null)
            //{
            //    return Problem("Entity set 'PlacementAppContext.Applys'  is null.");
            //}
            //  _context.Applys.Add(apply);
            //  await _context.SaveChangesAsync();
            // int? variable= HttpContext.Session.GetInt32("RoleId");
            // apply.RoleId = variable;
            if (await _context.Apply(apply) == null)
            { return BadRequest(); }
            
            else { return await _context.Apply(apply); }
            //await _context.Apply(apply);

            //return CreatedAtAction("GetApply", new { id = apply.Id }, apply);
        }

        // DELETE: api/Applies/5

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApply(int id)
        {
            //if (_context.Applys == null)
            //{
            //    return NotFound();
            //}
            //var apply = await _context.Applys.FindAsync(id);
            //if (apply == null)
            //{
            //    return NotFound();
            //}

            //_context.Applys.Remove(apply);
            //await _context.SaveChangesAsync();
            _context.DeleteApply(id);
            return NoContent();
        }

        private bool ApplyExists(int id)
        {
            return _context.ApplyExists(id);
        }
    }
}