using FakeItEasy;
using FluentAssertions;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Moq;
using PlacementApplicationNew.Controllers;
using PlacementApplicationNew.Model;
using PlacementApplicationNew.Repository;
using PlacementApplicationNew.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacementApplicationNew.Test
{
    public class StudentControllerTest
    {
        private readonly Mock<IStudent> mockStudentInter = new Mock<IStudent>();

        private readonly StudentsController _studentcontroller;
        public StudentControllerTest()
        {

            //adminrepo = A.Fake<IAdmin>();
            _studentcontroller = new StudentsController(mockStudentInter.Object);

        }
        [Fact]
        public async Task GetAllStudents_ReturnsListOfStudents_WhenStudentsExistInListAsync()
        {
            //Arrange
            var prod = await GetSampleStudents();
            mockStudentInter.Setup(x => x.GetStudents()).ReturnsAsync(prod);
            //Act
            var actionResult = await _studentcontroller.GetStudents();
            //OkObjectResult result = actionResult.Result as OkObjectResult;
            int actual = actionResult.Value.Count();
            int expected = prod.Count;

            //int expected = await ; 
            // var expected = await GetSampleCompanies.Count();
            //Assert
            //Assert.IsType<OkObjectResult>(result);
            //Assert.Equal((await GetSampleCompanies()),actual);
            Assert.Equal(expected, actual);

        }
        [Fact]
        public async Task GetStudentById_ReturnStudentObject_WhenStudentwithSpecificeIdExists()
        {
            //arrange
            var product = await GetSampleStudents();
            //var company =  product.Result[0];
            var student = product[0];
            mockStudentInter.Setup(x => x.GetStudent(1)).ReturnsAsync(student);
            //act
            var actionResult = await _studentcontroller.GetStudent(1);
            var result = actionResult.Value;

            //Assert

            Assert.Equal(student, result);
        }
        [Fact]
        public async Task AddNewStudentToCreate()
        {
            //Arrange
            var student = new Student
            {
                UserId = 3,
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
            mockStudentInter.Setup(x => x.AddNewStudent(student))       //mocking the create method in interface
           .ReturnsAsync(student);

            var prod = await GetSampleStudents();           //add new pizza to list
            prod.Add(student);
            var thirdcompany = prod[2];
            //Act
            var actionResult = await _studentcontroller.PostStudent(student);
            //Assert
            //var result = actionResult.Result as CreatedResult;
            //var actual = result.Value as Pizza;
            actionResult.Equals(thirdcompany);
                     
            // Assert.IsType<CreatedResult>(result);
        }
        [Fact]
        public async Task DeleteStudent_ReturnDeleteStatusAsync()
        {
            List<Student> product = await GetSampleStudents();
            //var company =  product.Result[0];
            var role = product[0];
            mockStudentInter.Setup(x => x.DeleteStudent(role.UserId)); //mocking the create method in interface
            var actionResult = await _studentcontroller.DeleteStudent(role.UserId);
            //mockProductInter.Setup(x => x.GetCompanies()).ReturnsAsync(product);
            //var result = await _companyController.GetCompanies();
            actionResult.Should().BeOfType<NoContentResult>();
        }
        [Fact]
        public async Task EditStudent_ReturnEditedStatus_WhenPassingStudentObjectAsync()
        {

            var prod = await GetSampleStudents();
            var l = prod[0];
            l.FirstName = "Michael";
            mockStudentInter.Setup(x => x.UpdateStudent(l)).ReturnsAsync(l);       //mocking the create method in interface           
            var e = prod[0];
            _studentcontroller.PutStudent(l.UserId, l);
            var firstProduct = prod[0];
            mockStudentInter.Setup(x => x.GetStudent(l.UserId))
                .ReturnsAsync(firstProduct);
            //act
            var actionResult = _studentcontroller.GetStudent(l.UserId);
            actionResult.Equals(e);
        }

        private async Task<List<Student>> GetSampleStudents()
        {
            //Arrange
            var output = new List<Student>
            {
                  new Student
                  { UserId = 1,
                    FirstName = "Antony",
                    LastName = "Sahaya Michael",
                    FatherName = "Michael",
                    BranchName = "CSE",
                    GraduationYear = new DateTime(2019, 10, 20),
                    Class10thMarks = 90,
                    Class12thMarks = 90,
                    CurrentCgpa = 8,
                    Password = "Anto@123"

                },
                  new Student {
                    UserId = 2,
                    FirstName = "Antony",
                    LastName = "Sahaya Michael",
                    FatherName = "Michael",
                    BranchName = "CSE",
                    GraduationYear = new DateTime(2019, 10, 20),
                    Class10thMarks = 90,
                    Class12thMarks = 90,
                    CurrentCgpa = 8,
                    Password = "Anto@123"

                  },
                  
                 };
            return output;
        }
        [Fact]
        public async Task StudentsController_Student_Login_ReturnsAcceptRequestAsync()
        {
            var sample = await GetSampleStudents();
            var s = sample[0];
            StudentToken l = new StudentToken()
            {
                student = new Student
                {
                    UserId = 1,
                    FirstName = "Antony",
                    LastName = "Sahaya Michael",
                    FatherName = "Michael",
                    BranchName = "CSE",
                    GraduationYear = new DateTime(2019, 10, 20),
                    Class10thMarks = 90,
                    Class12thMarks = 90,
                    CurrentCgpa = 8,
                    Password = "Anto@123"
                },
                studentToken = "nkjhuirhiojhoi;ejo;iyj459uy5oi4j54io",
            };
            // Act
            mockStudentInter.Setup(x => x.Login(s)).ReturnsAsync(l);
            var result = await _studentcontroller.Login(s);
            result.Value.Should().NotBeNull(); 
            
            //var admin = result.Result as AcceptedResult;

            //result.Should().BeOfType<OkObjectResult>();
            //Assert
            //Assert.Equal(StatusCodes.Status202Accepted, admin);
           // Assert.IsType<AcceptedResult>(admin);
        }
        //private readonly IStudent _studentRepository;
        //public StudentControllerTest()
        //{
        //    _studentRepository = A.Fake<IStudent>();
        //}
        //[Fact]
        //public async Task StudentController_GetStudents_ListStudentAsync()
        //{
        //    var prodlist = new List<Student>();
        //    A.CallTo(() => _studentRepository.GetStudents()).Returns(prodlist);
        //    var productsController = new StudentsController(_studentRepository);
        //    // var expected = A.Fake<ActionResult<List<Product>>>();
        //    //Act
        //    var result = productsController.GetStudents();
        //    //Assert
        //    // Assert.Equal(expected, result);
        //    result.Should().NotBeNull();
        //    //result.Should().BeOfType<ActionResult<List<Student>>>();
        //}
        //[Fact]
        //public async Task StudentController_GetStudent()
        //{
        //    //Arrange
        //    var studentId = 1;
        //    Student student = new Student
        //    {
        //        UserId = studentId,
        //        FirstName = "Antony",
        //        LastName = "Sahaya Michael",
        //        FatherName = "Michael",
        //        BranchName = "CSE",
        //        GraduationYear = new DateTime(2019, 10, 20),
        //        Class10thMarks = 90,
        //        Class12thMarks = 90,
        //        CurrentCgpa = 8,
        //        Password = "Anto@123"
        //    };
        //    A.CallTo(() => _studentRepository.GetStudent(1)).Returns(student);
        //    var StudentController = new StudentsController(_studentRepository);

        //    //Act
        //    ActionResult<Student> TempResult = await StudentController.GetStudent(studentId);
        //    var result = TempResult.Value;
        //    //Assert

        //    result.Should().BeOfType<Student>();



        //}
        //[Fact]
        //public async Task PutStudent_ReturnStudent()
        //{
        //    //Arrange
        //    var Id = 1;
        //    Student student = new Student
        //    {
        //        UserId = Id,
        //        FirstName = "Antony",
        //        LastName = "Sahaya Michael",
        //        FatherName = "Michael",
        //        BranchName = "CSE",
        //        GraduationYear = new DateTime(2019, 10, 30),
        //        Class10thMarks = 90,
        //        Class12thMarks = 90,
        //        CurrentCgpa = 9,
        //        Password = "Anto@123"
        //    };
        //    A.CallTo(() => _studentRepository.UpdateStudent(student)).Returns(student);
        //    var StudentController = new StudentsController(_studentRepository);
        //    //Act
        //    var TempResult = await StudentController.PutStudent(Id,student);
        //    A.CallTo(() => _studentRepository.GetStudent(Id)).Returns(student);
        //    ActionResult<Student> Result = await StudentController.GetStudent(Id);
        //    var result = Result.Value;
        //    result.Should().BeEquivalentTo(student);
        //    //ObjectResult objectResponse = Assert.IsType<ObjectResult>(TempResult);

        //    //var result = TempResult as OkObjectResult;
        //    // Student result = okResult.Value as Configuration;
        //    //Assert
        //    //Assert.Equal(NoContentResult,TempResult);
        //    //result.Should().BeOfType<OkObjectResult>();
        //    //name.Should().BeEquivalentTo(result.FirstName);
        //    //result.Should().BeSameAs(name.Equals(result.FirstName));
        //    TempResult.Should().BeOfType<NoContentResult>();

        //}
        //[Fact]
        //public async Task StudentController_DeleteProduct_ReturnOk200Async()
        //{
        //    var Id = 1;
        //    Student student = new Student
        //    {
        //        UserId = Id,
        //        FirstName = "Antony",
        //        LastName = "Sahaya Michael",
        //        FatherName = "Michael",
        //        BranchName = "CSE",
        //        GraduationYear = new DateTime(2019, 10, 20),
        //        Class10thMarks = 90,
        //        Class12thMarks = 90,
        //        CurrentCgpa = 9,
        //        Password = "Anto@123"
        //    };
        //    A.CallTo(() => _studentRepository.DeleteStudent(Id));//repo method
        //    var dobj = new StudentsController(_studentRepository);
        //    var TempResult=await dobj.DeleteStudent(Id);
        //    TempResult.Should().BeOfType<NoContentResult>();

        //}


    }
}
