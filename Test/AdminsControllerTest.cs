using AutoFixture;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PlacementApplicationNew.Controllers;
using PlacementApplicationNew.Model;
using PlacementApplicationNew.Repository;

namespace PlacementApplicationNew.Test
{
    public class AdminsControllerTest
    {
        private readonly Mock<IAdmin> mockLoginInter = new Mock<IAdmin>();

        private readonly AdminsController _admincontroller;
        public AdminsControllerTest()
        {

            //adminrepo = A.Fake<IAdmin>();
            _admincontroller = new AdminsController(mockLoginInter.Object);

        }
        [Fact]
        public async Task AdminsController_Admin_Login_ReturnsAcceptRequestAsync()
        {
            var sample = await GetSampleAdmins();
            var l = sample[1];           
            // Act
            mockLoginInter.Setup(x => x.Login(l)).ReturnsAsync(l);
            var result=await _admincontroller.Login(l);
            var admin = result.Result as AcceptedResult;

            //result.Should().BeOfType<OkObjectResult>();
            //Assert
            //Assert.Equal(StatusCodes.Status202Accepted, admin);
            Assert.IsType<AcceptedResult>(admin);
        }
        [Fact]
        public async Task AdminsController_Admin_Login_ReturnsBadRequestAsync()
        {
            var sample = await GetSampleAdmins();
            Admin l = new Admin();
            l = null;
            // Act
            mockLoginInter.Setup(x => x.Login(l)).ReturnsAsync(l);
            var result = await _admincontroller.Login(l);
            var admin = result.Result as BadRequestResult;

            //result.Should().BeOfType<OkObjectResult>();
            //Assert
            //Assert.Equal(StatusCodes.Status202Accepted, admin);
            Assert.IsType<BadRequestResult>(admin);
        }
        private async Task<List<Admin>> GetSampleAdmins()
        {
            List<Admin> output = new List<Admin>
            {
                new Admin
                {
                UserId=1,
                UserName = "Admin",
                Password = "Antony@0123"
               },
                new Admin {
                UserId=2,
                UserName = "Admin",
                Password = "Antony@0123"
                }
            };
            return output;
        }
    }
}
