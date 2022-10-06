using PlacementApplicationNew.Model;

namespace PlacementApplicationNew.Repository
{
    public interface ICompany
    {
        public Task<List<Company>> GetCompanies();
        public Task<Company> GetCompany(int id);
        public Task<Company> AddNewCompany(Company company);
        public Task<Company> UpdateCompany(Company company);
        public void DeleteCompany(int id);
        public bool CompanyExists(int id);

    }

}
