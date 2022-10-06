using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlacementApplicationNew.Model;
using PlacementApplicationNew.Repository;

namespace PlacementApplicationNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly IAdmin placement;

        public AdminsController(IAdmin _placement)
        {
            placement = _placement;
        }


      

        //// GET: api/Admins/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Admin>> GetAdmin(int id)
        //{
        //  if (_context.Admins == null)
        //  {
        //      return NotFound();
        //  }
        //    var admin = await _context.Admins.FindAsync(id);

        //    if (admin == null)
        //    {
        //        return NotFound();
        //    }

        //    return admin;
        //}

        //// PUT: api/Admins/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutAdmin(int id, Admin admin)
        //{
        //    if (id != admin.UserId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(admin).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!AdminExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        [HttpPost("Login")]
        public async Task<ActionResult<Admin>> Login(Admin admin)
        {
            if (await placement.Login(admin) == null)
            { return BadRequest(); }

            else { return Accepted();
            }
        }

        // POST: api/Admins
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Admin>> PostAdmin(Admin admin)
        {
            await placement.AddNewAdmin(admin);
            return admin;
           // return CreatedAtAction("GetStudent", new { id = admin.UserId }, admin);
        }

        // DELETE: api/Admins/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAdmin(int id)
        //{
        //    if (_context.Admins == null)
        //    {
        //        return NotFound();
        //    }
        //    var admin = await _context.Admins.FindAsync(id);
        //    if (admin == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Admins.Remove(admin);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}
        //private bool AdminExists(int id)
        //{
        //    return (_context.Admins?.Any(e => e.UserId == id)).GetValueOrDefault();
        //}

    }
}
