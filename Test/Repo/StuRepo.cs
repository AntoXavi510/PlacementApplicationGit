using Microsoft.EntityFrameworkCore;
using PlacementApplicationNew.Model;
using FluentAssertions;
using PlacementApplicationNew.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace PlacementApplicationNew.Test.Repo
{
    public class StuRepo
    {
        private readonly IConfiguration _configuration;

        private async Task<PlacementAppContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<PlacementAppContext>()
                            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                            .Options;
            var databaseContext = new PlacementAppContext(options);
            databaseContext.Database.EnsureCreated();
            var Temp = 100;
            if (await databaseContext.Students.CountAsync() <= 0)
            {
                
                    databaseContext.Students.Add(
                        new Student
                        {
                            UserId = Temp++,
                            FirstName = "Antony",
                            LastName = "Sahaya Michael",
                            FatherName = "Michael",
                            BranchName = "CSE",
                            GraduationYear = new DateTime(2019, 10, 20),
                            Class10thMarks = 90,
                            Class12thMarks = 90,
                            CurrentCgpa = 8,
                            Password = "Anto@123"
                        }

                        );
                databaseContext.Students.Add(
                        new Student
                        {
                            UserId = Temp++,
                            FirstName = "Antony",
                            LastName = "Sahaya Michael",
                            FatherName = "Michael",
                            BranchName = "CSE",
                            GraduationYear = new DateTime(2019, 10, 20),
                            Class10thMarks = 90,
                            Class12thMarks = 90,
                            CurrentCgpa = 8,
                            Password = "Anto@123"
                        }

                        );
                databaseContext.Students.Add(
                        new Student
                        {
                            UserId = Temp++,
                            FirstName = "Antony",
                            LastName = "Sahaya Michael",
                            FatherName = "Michael",
                            BranchName = "CSE",
                            GraduationYear = new DateTime(2019, 10, 20),
                            Class10thMarks = 90,
                            Class12thMarks = 90,
                            CurrentCgpa = 8,
                            Password = "Anto@123"
                        }

                        );

                await databaseContext.SaveChangesAsync();

                
            }
            return databaseContext;
        }
        [Fact]
        public async Task StudentRepo_GetAllStudents_ReturnStudents()
        {
            //Arrange
            var dbContext = await GetDatabaseContext(); //This one calls the inmemory database
            IStudent studentsRepository = new StudentRepo(dbContext,_configuration); //repo layer object calling
            //Act
            var result = studentsRepository.GetStudents(); //calling the methods of repository
                                                            //Assert
            var i = dbContext.Students.Count();
            var count = result.Result.Count();
            //result.Should().HaveCount(i);
            Assert.Equal(i, count);
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task LoginRepo_UserLogin_ReturnCustomerDetails()
        {
            //Arrange
            
            var dbContext = await GetDatabaseContext();
            var student = new Student
            {
                UserId = 100,
                FirstName = "Antony",
                LastName = "Sahaya Michael",
                FatherName = "Michael",
                BranchName = "CSE",
                GraduationYear = new DateTime(2019, 10, 20),
                Class10thMarks = 90,
                Class12thMarks = 90,
                CurrentCgpa = 8,
                Password = "Anto@123"
            };
            //student.UserId = 100;
            //student.Password = "Anto@123";
            //Act
    var myConfiguration = new Dictionary<string, string> { { "JWT:ValidAudience", "User" }, { "JWT:ValidIssuer", "http://localhost:42011" }, 
    { "JWT:Secret", "JWTAuthenticationHIGHsecuredPasswordVVVp1OH7Xzyr" } }; 
            var configuration = new ConfigurationBuilder().AddInMemoryCollection(myConfiguration).Build();

            IStudent studentsRepository = new StudentRepo(dbContext, configuration);

            var result = studentsRepository.Login(student).Result.student;
            //Assert
            student.FirstName.Should().Be(result.FirstName);
            
            result.Should().NotBeNull();
        }
        [Fact]
        public async Task StudentRepo_GetStudentById_ReturnStudentId()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            IStudent prodRepository = new StudentRepo(dbContext,_configuration);
            //Act
            var result = prodRepository.GetStudent(101);
            //Assert
            var name = "Antony";
            name.Should().Be(result.Result.FirstName);
        }
        [Fact]
        public async Task StudentRepo_UpdateStudent_ReturnEdit()
        {
            //Arrange
            //var prod=await GetDatabaseContext();
            
            var student = new Student()
            {
                UserId = 100,
                FirstName = "Antony",
                LastName = "Sahaya Michael",
                FatherName = "Michael",
                BranchName = "CSE",
                GraduationYear = new DateTime(2019, 10, 20),
                Class10thMarks = 90,
                Class12thMarks = 90,
                CurrentCgpa = 9,
                Password = "Anto@123"
            };
            var dbContext = await GetDatabaseContext();
            IStudent studentsRepository = new StudentRepo(dbContext, _configuration); //repo layer object calling
            //Act
            //---------------------------------------------------------
            var prodfind = await dbContext.Students.FindAsync(student.UserId);
            dbContext.Entry<Student>(prodfind).State = EntityState.Detached;//has to be used only on xUnittesting
            //----------------------------------------------------------
            var result = await studentsRepository.UpdateStudent(student);
            //Assert
            result.Should().BeEquivalentTo(student);
            var i = dbContext.Students.Count();
            var result1 = studentsRepository.GetStudents().Result.Count();
            result1.Should().Be(i);
        }
        [Fact]
        public async Task StudentRepo_DeleteStudent()
        {
            var id = 101;
           

            var dbContext = await GetDatabaseContext();
            IStudent dRepository = new StudentRepo(dbContext,_configuration);
            dRepository.DeleteStudent(id);
            var a = dRepository.GetStudent(id);
            a.Result.Should().BeNull();
            var i = dbContext.Students.Count();
            var result1 = dRepository.GetStudents().Result.Count();
            result1.Should().Be(i);
        }
        [Fact]
        public async Task AddRepo_AddStudent_ReturnStudent()
        {
            //Arrange
            var student = new Student()
            {
                UserId = 104,
                FirstName = "Antony",
                LastName = "Sahaya Michael",
                FatherName = "Michael",
                BranchName = "CSE",
                GraduationYear = new DateTime(2019, 10, 20),
                Class10thMarks = 90,
                Class12thMarks = 90,
                CurrentCgpa = 9,
                Password = "Anto@123"

            };
            var dbContext = await GetDatabaseContext();
            IStudent prodRepository = new StudentRepo(dbContext,_configuration);
            //Act
            var result = await prodRepository.AddNewStudent(student);
            //Assert
            result.Should().BeEquivalentTo(student);
            //dbContext.Companies.Should().HaveCount(3);
            var i = dbContext.Students.Count();
            var result1 = prodRepository.GetStudents().Result.Count();
            Assert.Equal(i, result1);

        }
    }
}
