using PlacementApplicationNew.Model;
namespace PlacementApplicationNew.Repository

{
    public interface IRoles
    {
        public Task<List<Role>> GetRoles();
        public Task<List<Role>> GetRolesForCompany(int id);
        public Task<Role> GetRole(int id);
        public Task<Role> AddNewRole(Role role);
        public Task<Role> UpdateRole(Role role);
        public void DeleteRole(int id);
        public bool RoleExists(int id);
    }
}
