using PlacementApplicationNew.Model;

namespace PlacementApplicationNew.Repository
{
    public interface IApply
    {
        public Task<List<Apply>> GetApplys();
        public Task<Apply> GetApply(int id);
        public Task<List<Apply>> GetRolesForStudent(int id);
        public Task<List<Apply>> GetApplyForRoles(int id);
        public Task<Apply> Apply(Apply apply);
        public void DeleteApply(int id);
        public bool ApplyExists(int id);

    }
}
