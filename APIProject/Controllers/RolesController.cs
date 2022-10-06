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
    public class RolesController : ControllerBase
    {
        private readonly IRoles _context;

        public RolesController(IRoles context)
        {
            _context = context;
        }

        // GET: api/Roles
        /// <summary>
        /// Roles available in  the list.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
          if (_context.GetRoles == null)
          {
              return NotFound();
          }
            return await _context.GetRoles();
        }
        /// <summary>
        /// Role of specified id.
        /// </summary>

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRole(int id)
        {
            //if (_context.Roles == null)
            //{
            //    return NotFound();
            //}
            //  var role = await _context.Roles.FindAsync(id);

            //  if (role == null)
            //  {
            //      return NotFound();
            //  }

            //  return role;
          //  HttpContext.Session.SetInt32("RoleId", id);
            return await _context.GetRole(id);
        }
        /// <summary>
        /// Update role of a specified id .
        /// </summary>
        // PUT: api/Roles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(int id, Role role)
        {
            //if (id != role.RoleId)
            //{
            //    return BadRequest();
            //}

            //_context.Entry(role).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!RoleExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return NoContent();
            await _context.UpdateRole(role);
            return NoContent();
        }
        /// <summary>
        /// Add A new role.
        /// </summary>
        // POST: api/Roles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Role>> PostRole(Role role)
        {
            //if (_context.Roles == null)
            //{
            //    return Problem("Entity set 'PlacementAppContext.Roles'  is null.");
            //}
            //  _context.Roles.Add(role);
            //  await _context.SaveChangesAsync();

            await _context.AddNewRole(role);
            return CreatedAtAction("GetRole", new { id = role.RoleId }, role);
        }
        /// <summary>
        /// Roles available in a company.
        /// </summary>
        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRolesForCompany(int id)
        {
            //if (_context.Applys == null)
            //{
            //    return NotFound();
            //}
            //  return await _context.Applys.ToListAsync();
            return await _context.GetRolesForCompany(id);
        }
        /// <summary>
        /// Delete Role of a company.
        /// </summary>
        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            //if (_context.Roles == null)
            //{
            //    return NotFound();
            //}
            //var role = await _context.Roles.FindAsync(id);
            //if (role == null)
            //{
            //    return NotFound();
            //}

            //_context.Roles.Remove(role);
            //await _context.SaveChangesAsync();
            _context.DeleteRole(id);
            return Ok(_context);
            
        }

        //private bool RoleExists(int id)
        //{
        //    return (_context.Roles?.Any(e => e.RoleId == id)).GetValueOrDefault();
        //}
    }
}
