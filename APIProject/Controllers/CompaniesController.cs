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
    public class CompaniesController : ControllerBase
    {
        private readonly ICompany placement;

        public CompaniesController(ICompany _placement)
        {
            placement = _placement;
        }
        // GET: api/Companies
        /// <summary>
        /// Shows List of Companies.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompanies()
        {
            return await placement.GetCompanies();
        }

        // GET: api/Companies/5
        /// <summary>
        /// Shows a Specific Company.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompany(int id)
        {
            
            return await placement.GetCompany(id);
        }

        // PUT: api/Companies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update a Specific Company.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(int id, Company company)
        {
            //    if (id != company.CompanyId)
            //    {
            //        return BadRequest();
            //    }

            //    _context.Entry(company).State = EntityState.Modified;

            //    try
            //    {
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!CompanyExists(id))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }

            //    return NoContent();
            //
            await placement.UpdateCompany(company);
            return CreatedAtAction("GetCompany", new { id = company.CompanyId }, company);
        }

        // POST: api/Companies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add a Company.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Company>> PostCompany(Company company)
        {
            //if (_context.Companies == null)
            //{
            //    return Problem("Entity set 'PlacementAppContext.Companies'  is null.");
            //}
            //  _context.Companies.Add(company);
            //  await _context.SaveChangesAsync();

            //  return CreatedAtAction("GetCompany", new { id = company.CompanyId }, company);
            if (await placement.AddNewCompany(company) == null)
            { return BadRequest(); }

            else { return Accepted(); }

            await placement.AddNewCompany(company);
            return CreatedAtAction("GetCompany", new { id = company.CompanyId}, company);
        }

        // DELETE: api/Companies/5
        /// <summary>
        /// Delete a Company.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            //if (_context.Companies == null)
            //{
            //    return NotFound();
            //}
            //var company = await _context.Companies.FindAsync(id);
            //if (company == null)
            //{
            //    return NotFound();
            //}

            //_context.Companies.Remove(company);
            //await _context.SaveChangesAsync();

            //return NoContent();
            placement.DeleteCompany(id);
            return Ok(placement);
        }

        private bool CompanyExists(int id)
        {
            return placement.CompanyExists(id);
        }
    }
}
