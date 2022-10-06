using PlacementApplicationNew.Model;
using PlacementApplicationNew.Token;

namespace PlacementApplicationNew.Repository
{
    public interface IStudent
    {
        public Task<List<Student>> GetStudents();
        public Task<Student> GetStudent(int id);
        public Task<StudentToken> Login(Student student);
        public Task<Student> AddNewStudent(Student student);
        public Task<Student> UpdateStudent(Student student);
        public void DeleteStudent(int id);
        public bool StudentExists(int id);
       
    }
}
