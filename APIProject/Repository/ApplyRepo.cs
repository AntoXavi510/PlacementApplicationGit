using Microsoft.EntityFrameworkCore;
using PlacementApplicationNew.Model;

namespace PlacementApplicationNew.Repository
{
    public class ApplyRepo : IApply
    {
        private readonly PlacementAppContext _context;
        public ApplyRepo(PlacementAppContext context)
        {
            _context = context;
        }
        public async  Task<Apply> Apply(Apply apply)
        { 
            var result = (from i in _context.Applys where i.StudentId == apply.StudentId && i.RoleId == apply.RoleId select i).FirstOrDefault();
            var students = _context.Students.Where(p => p.UserId == apply.StudentId).Select(p => p.CurrentCgpa).FirstOrDefault();
            var roles = _context.Roles.Where(p => p.RoleId == apply.RoleId).Select(p => p.CutoffPercentage).FirstOrDefault();
            try
            {
                if (result == null && students >= roles)
                {
                    _context.Applys.Add(apply);
                    await _context.SaveChangesAsync();
                    return apply;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                throw ;
            }
            return apply;
        }

        public bool ApplyExists(int id)
        {
            return (_context.Applys?.Any(e => e.Id == id)).GetValueOrDefault();

        }
        public async Task<List<Apply>> GetRolesForStudent(int id)
        {
            Student student = new Student();
            List<Apply> result = await (from i in _context.Applys.Include(x => x.Student).Include(x => x.Role).ThenInclude(x=>x.Company) where i.StudentId == id select i).ToListAsync();
            return result;
        }
        public void DeleteApply(int id)
        {
            Apply apply = _context.Applys.Find(id);
            _context.Remove(apply);
            _context.SaveChanges();
        }

        public async Task<Apply> GetApply(int id)
        {
            var apply = await _context.Applys.Include(x => x.Student).Include(x => x.Role).ThenInclude(x => x.Company).Where(y => y.Id == id).FirstOrDefaultAsync();
            return apply;
        }
        public async Task<List<Apply>> GetApplyForRoles(int id)
        {
            var apply = await _context.Applys.Include(x => x.Student).Include(x => x.Role).ThenInclude(x => x.Company).Where(y => y.RoleId == id).ToListAsync();
            return apply;
        }
        public async Task<List<Apply>> GetApplys()
        {
            return await _context.Applys.Include(x => x.Student).Include(x => x.Role).ThenInclude(x => x.Company).OrderBy(s => s.Role.RoleId).ToListAsync();
        }

     
    }
}
