using PlacementApplicationNew.Model;

namespace PlacementApplicationNew.Repository
{
    public interface IAdmin
    {
        
        public Task<Admin> GetAdmin(int id);
        public Task<Admin> Login(Admin admin);
        public Task<Admin> AddNewAdmin(Admin admin);        
        public Task DeleteAdmin(int id);
    }
}
