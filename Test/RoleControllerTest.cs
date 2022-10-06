using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NuGet.Protocol;
using PlacementApplicationNew.Controllers;
using PlacementApplicationNew.Model;
using PlacementApplicationNew.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacementApplicationNew.Test
{
    public class RoleControllerTest
    {
        private readonly RolesController _roleController;
        private readonly Mock<IRoles> mockRoleInter = new Mock<IRoles>();
        public RoleControllerTest()
        {
            _roleController = new RolesController(mockRoleInter.Object);
        }
        [Fact]
        public async Task GetAllRoles_ReturnsListOfRoles_WhenRoleExistInListAsync()
        {
            //Arrange
            var prod = GetSampleRoles();
            mockRoleInter.Setup(x => x.GetRoles()).Returns(prod);
            //Act
            var actionResult = await _roleController.GetRoles();
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
        public async Task GetRoleById_ReturnRoleObject_WhenRolewithSpecificeIdExists()
        {
            //arrange
            var product = await GetSampleRoles();
            //var company =  product.Result[0];
            var company = product[0];
            mockRoleInter.Setup(x => x.GetRole(1)).ReturnsAsync(company);
            //act
            var actionResult = await _roleController.GetRole(1);
            var result = actionResult.Value;

            //Assert

            Assert.Equal(company, result);
        }
        [Fact]
        public async Task AddNewRoleToCreate()
        {
            //Arrange
            var role = new Role
            {
                RoleId=3,
                RoleName="SoftwareDeveloper",
                SalaryPackage=300000,
                CutoffPercentage=9,
                DateOfDrive=new DateTime(2024,10,10),
                Location="Chennai",
                CompanyID=1
                

            };
            mockRoleInter.Setup(x => x.AddNewRole(role))       //mocking the create method in interface
           .ReturnsAsync(role);

            var prod = await GetSampleRoles();           //add new pizza to list
            prod.Add(role);
            var thirdrole = prod[2];
            //Act
            var actionResult = await _roleController.PostRole(role);
            //Assert
            //var result = actionResult.Result as CreatedResult;
            //var actual = result.Value as Pizza;
            actionResult.Equals(thirdrole);
            // Assert.IsType<CreatedResult>(result);
        }
        [Fact]
        public async Task DeleteRole_ReturnDeleteStatusAsync()
        {
            List<Role> product = await GetSampleRoles();
            //var company =  product.Result[0];
            var role = product[0];
            mockRoleInter.Setup(x => x.DeleteRole(role.RoleId)); //mocking the create method in interface
            var actionResult = await _roleController.DeleteRole(role.RoleId);
            //mockProductInter.Setup(x => x.GetCompanies()).ReturnsAsync(product);
            //var result = await _companyController.GetCompanies();
            actionResult.Should().BeOfType<OkObjectResult>();
        }
        [Fact]
        public async Task EditRole_ReturnEditedStatus_WhenPassingRoleObjectAsync()
        {

            var prod = await GetSampleRoles();
            var l = prod[0];
            l.RoleName = "DataEngineer";
            mockRoleInter.Setup(x => x.UpdateRole(l)).ReturnsAsync(l);       //mocking the create method in interface           
            var e = prod[0];
            _roleController.PutRole(l.RoleId, l);
            var firstProduct = prod[0];
            mockRoleInter.Setup(x => x.GetRole(l.RoleId))
                .ReturnsAsync(firstProduct);
            //act
            var actionResult = _roleController.GetRole(1);
            actionResult.Equals(e);
        }
        [Fact]
        public async Task GetRoleByCompanyId_ReturnRoleObject_WhenCompanywithSpecificeIdExists()
        {
            //arrange
            var company = await GetSampleRoles();
            var demo =(from i in company where i.CompanyID == 1 select i).ToList();
            //var product = company[0];
            mockRoleInter.Setup(x => x.GetRolesForCompany(1)).ReturnsAsync(demo);
            //act
            var actionResult = await _roleController.GetRolesForCompany(1);
            var result = actionResult.Value;

            //Assert
            //var obj=actionResult.;
            Assert.Equal(demo, result);
        }
        private async Task<List<Role>> GetSampleRoles()
        {
            //Arrange
            var output = new List<Role>
            {
                  new Role
                  { RoleId=1,RoleName="SoftwareDeveloper",SalaryPackage=300000,CutoffPercentage=9,DateOfDrive=new DateTime(2024,10,10),Location="Chennai",CompanyID=1
                },
                  new Role {
                     RoleId=2,RoleName="SoftwareDeveloper",SalaryPackage=300000,CutoffPercentage=9,DateOfDrive=new DateTime(2024,10,10),Location="Chennai",CompanyID=1
                  },
                  new Role {
                     RoleId=3,RoleName="SoftwareDeveloper",SalaryPackage=300000,CutoffPercentage=9,DateOfDrive=new DateTime(2024,10,10),Location="Chennai",CompanyID=2
                  },
                 };
            return output;
        }

    }
}
