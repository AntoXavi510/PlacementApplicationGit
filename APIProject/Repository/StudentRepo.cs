using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PlacementApplicationNew.Model;
using PlacementApplicationNew.Token;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PlacementApplicationNew.Repository
{
    public class StudentRepo : IStudent
    {
        private readonly PlacementAppContext _context;
        private readonly IConfiguration _configuration;
        public StudentRepo(PlacementAppContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<Student> AddNewStudent(Student student)
        {
            if (_context.Students.Any(ac => ac.UserId.Equals(student.UserId)))
            {
                return null;
            }
            else {
                _context.Students.Add(student);
                await _context.SaveChangesAsync();
                return student;
            }
        }
        public void DeleteStudent(int id)
        {
            Student student = _context.Students.Find(id);
            _context.Remove(student);
            _context.SaveChanges();
        }
        public async Task<Student> GetStudent(int id)
        {
            return await _context.Students.FindAsync(id);
        }       
        public async Task<List<Student>> GetStudents()
        {
                return await _context.Students.ToListAsync();            
        }
        public async Task<Student> UpdateStudent(Student student)
        {
            _context.Update(student);
            await _context.SaveChangesAsync();
            return student;
        }
       public  bool StudentExists(int id)
        {
            return (_context.Students?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
        //public async Task<StudentToken> Login(Student student)
        //{

        //    var result = await (from i in _context.Students where i.Password == student.Password && i.UserId == student.UserId select i).SingleOrDefaultAsync();
        //    try
        //    {
        //        if (result != null)
        //        {
        //            return result;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    catch (Exception ex) {
        //        throw;
        //    }
        //    return null;
        //}
        
public async Task<StudentToken> Login(Student student)
        {
            StudentToken st = new StudentToken(); 
            var result = await (from i in _context.Students where i.Password == student.Password && i.UserId == student.UserId select i).SingleOrDefaultAsync(); 
            st.student = result;
            if (result != null)
            {
                var authClaims = new List<Claim> { new Claim(ClaimTypes.SerialNumber,student.Password), new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), };

                var token = GetToken(authClaims);
                string s = new JwtSecurityTokenHandler().WriteToken(token);
                st.studentToken = s;
                st.student = result;
                return st;

            }
            return null;
        }
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));



            var token = new JwtSecurityToken(
                 issuer: _configuration["JWT:ValidIssuer"],
                 audience: _configuration["JWT:ValidAudience"],
                 expires: DateTime.Now.AddMinutes(30),
                 claims: authClaims,
                 signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                 ); ;



            return token;
        }







            //public async Task<Student> Login(Student student)
            //{
            //    var result = (from i in _context.Students where i.UserId == student.UserId && i.Password == student.Password select i).FirstOrDefault();
            //    return await Task.FromResult(result);
            //}
        }
    }
