using Moq;
using PlacementApplicationNew.Controllers;
using PlacementApplicationNew.Model;
using PlacementApplicationNew.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PlacementApplicationNew.Test
{
    public class ApplyControllerTest
    {
        private readonly AppliesController _applyController;
        private readonly Mock<IApply> mockApplyInter = new Mock<IApply>();
        public ApplyControllerTest()
        {
            _applyController = new AppliesController(mockApplyInter.Object);
        }
        [Fact]
        public async Task GetAllCompanies_ReturnsListOfCompanies_WhenCompanyExistInListAsync()
        {
            //Arrange
            var prod = GetSampleApplies();
            mockApplyInter.Setup(x => x.GetApplys()).Returns(prod);
            //Act
            var actionResult = await _applyController.GetApplys();
            //OkObjectResult result = actionResult.Result as OkObjectResult;
            int actual = actionResult.Value.Count();
            int expected = prod.Result.Count;

            //int expected = await ; 
            // var expected = await GetSampleCompanies.Count();
            //Assert
            //Assert.IsType<OkObjectResult>(result);
            //Assert.Equal((await GetSampleCompanies()),actual);
            Assert.Equal(expected, actual);

        }
        [Fact]
        public async Task GetApplyById_ReturnApplyObject_WhenApplywithSpecificeIdExists()
        {
            //arrange
            var product = await GetSampleApplies();
            //var company =  product.Result[0];
            var apply = product[0];
            mockApplyInter.Setup(x => x.GetApply(1)).ReturnsAsync(apply);
            //act
            var actionResult = await _applyController.GetApply(1);
            var result = actionResult.Value;

            //Assert

            Assert.Equal(apply, result);
        }
        [Fact]
        public async Task GetApplyByStudentId_ReturnApplyObject_WhenApplywithSpecificeIdExists()
        {
            //arrange
            var product = await GetSampleApplies();
            //var company =  product.Result[0];
            //var apply = product[0];
            var apply = (from i in product where i.StudentId == 1 select i).ToList();
            mockApplyInter.Setup(x => x.GetRolesForStudent(1)).ReturnsAsync(apply);
            //act
            var actionResult = await _applyController.GetRolesForStudent(1);
            var result = actionResult.Value;

            //Assert

            Assert.Equal(apply, result);
        }
        [Fact]
        public async Task GetApplyByRoleId_ReturnApplyObject_WhenApplywithSpecificeIdExists()
        {
            //arrange
            var product = await GetSampleApplies();
            //var company =  product.Result[0];
            //var apply = product[0];
            var apply = (from i in product where i.RoleId == 1 select i).ToList();
            mockApplyInter.Setup(x => x.GetApplyForRoles(1)).ReturnsAsync(apply);
            //act
            var actionResult = await _applyController.GetApplyForRoles(1);
            var result = actionResult.Value;

            //Assert

            Assert.Equal(apply, result);
        }
        [Fact]
        public async Task AddNewRoleToCreate()
        {
            //Arrange
            var apply = new Apply
            {
                Id = 3,
                RoleId = 1,
                StudentId = 2,
            };
            mockApplyInter.Setup(x => x.Apply(apply))       //mocking the create method in interface
           .ReturnsAsync(apply);

            var prod = await GetSampleApplies();           //add new pizza to list
            prod.Add(apply);
            var thirdapply = prod[2];
            //Act
            var actionResult = await _applyController.PostApply(apply);
            //Assert
            //var result = actionResult.Result as CreatedResult;
            //var actual = result.Value as Pizza;
            actionResult.Equals(thirdapply);
            // Assert.IsType<CreatedResult>(result);
        }
        private async Task<List<Apply>> GetSampleApplies()
        {
            //Arrange
            var output = new List<Apply>
            {
                  new Apply
                  { Id=1,RoleId=1,StudentId=1
                },
                  new Apply {
                     Id=2,RoleId=1,StudentId=3
                  },
                  new Apply {
                     Id=3,RoleId=3,StudentId=1
                  },
                 };
            return output;
        }
    }
}
