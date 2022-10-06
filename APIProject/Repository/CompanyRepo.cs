using Microsoft.EntityFrameworkCore;
using PlacementApplicationNew.Model;

namespace PlacementApplicationNew.Repository
{
    public class CompanyRepo:ICompany
    {
        private readonly PlacementAppContext _context;
        public CompanyRepo(PlacementAppContext context)
        {
            _context = context;
        }

        public async  Task<List<Company>> GetCompanies()
        {
            return await _context.Companies.ToListAsync();

        }

        public async  Task<Company> GetCompany(int id)
        {
           
            return await _context.Companies.FindAsync(id);

        }

        public async Task<Company> AddNewCompany(Company company)
        {
            if (_context.Companies.Any(ac => ac.CompanyName.Equals(company.CompanyName)))
            {
                return null;
            }
            else {
                await _context.Companies.AddAsync(company);
                await _context.SaveChangesAsync();
                return company;
            }
            
        }

        public async  Task<Company> UpdateCompany(Company company)
        {
            _context.Update(company);
            await _context.SaveChangesAsync();
            return company; ;
        }

        public void DeleteCompany(int id)
        {
            Company company = _context.Companies.Find(id);
            _context.Remove(company);
            _context.SaveChanges();

        }

        public bool CompanyExists(int id)
        {
            return (_context.Companies?.Any(e => e.CompanyId == id)).GetValueOrDefault();
        }

    }
}
