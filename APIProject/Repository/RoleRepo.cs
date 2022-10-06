using Microsoft.EntityFrameworkCore;
using PlacementApplicationNew.Model;
using System.Linq;

namespace PlacementApplicationNew.Repository
{
    public class RoleRepo : IRoles
    {
        private readonly PlacementAppContext _role;
        public RoleRepo(PlacementAppContext role)
        {
            _role = role;
        }
        public async Task<Role> AddNewRole(Role role)
        {
            _role.Roles.Add(role);
            await _role.SaveChangesAsync();
            return role;
        }
        public void DeleteRole(int id)
        {
           // Role role = _context.Roles.Where(y=>y.RoleId ).FirstOrDefault();
             Role role = _role.Roles.Include(x=>x.Company).Where(y => y.RoleId == id).FirstOrDefault();       
            _role.Remove(role);
            _role.SaveChanges();
        }
        public async Task<Role> GetRole(int id)
        {  var role= await _role.Roles.Include(x => x.Company).Where(y => y.RoleId == id).FirstOrDefaultAsync();
            return role;
        }
        public async Task<List<Role>> GetRoles()
        {
          return  await _role.Roles.Include(x => x.Company).ToListAsync();
        }
        public async Task<List<Role>> GetRolesForCompany(int id)
        {
            var apply = await _role.Roles.Include(x => x.Company).Where(y => y.CompanyID == id).ToListAsync();
            return apply;
        }
        public bool RoleExists(int id)
        {
            throw new NotImplementedException();
        }
        public async  Task<Role> UpdateRole(Role role)
        {  _role.Update(role);
            await _role.SaveChangesAsync();
            return role;
        }
    }
}
