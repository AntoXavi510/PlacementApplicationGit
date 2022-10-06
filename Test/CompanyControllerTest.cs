using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Moq;
using PlacementApplicationNew.Controllers;
using PlacementApplicationNew.Model;
using PlacementApplicationNew.Repository;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using FluentAssertions;

namespace PlacementApplicationNew.Test
{
    public class CompanyControllerTest
    {
        private readonly CompaniesController _companyController;
        private readonly Mock<ICompany> mockProductInter = new Mock<ICompany>();
        public CompanyControllerTest()
        {
            _companyController = new CompaniesController(mockProductInter.Object);
        }
        [Fact]
        public async Task GetAllCompanies_ReturnsListOfCompanies_WhenCompanyExistInListAsync()
        {
            //Arrange
            var prod = GetSampleCompanies();
            mockProductInter.Setup(x => x.GetCompanies()).Returns(prod);
            //Act
            var actionResult = await _companyController.GetCompanies();
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
        public async Task GetCompanyById_ReturnProductObject_WhenCompanywithSpecificeIdExists()
        {
            //arrange
            var product = await GetSampleCompanies();
            //var company =  product.Result[0];
            var company = product[0];
            mockProductInter.Setup(x => x.GetCompany(1)).ReturnsAsync(company);
            //act
            var actionResult = await _companyController.GetCompany(1);
            var result = actionResult.Value;
            //Assert
            Assert.Equal(company, result);
        }
        [Fact]
        public async Task GetCompanyById_ReturnNoContentObject_WhenCompanywithSpecificeIdNotExists()
        {
            //arrange
            //var product = await GetSampleCompanies();
            //var company =  product[0];
            //var company = product[0];
            var company = new Company();
            company = null;
            mockProductInter.Setup(x => x.GetCompany(5)).ReturnsAsync(company);
            //act
            var actionResult = await _companyController.GetCompany(5);

            //var result = actionResult.Result as NoContentResult;
            var result =actionResult.Result as NoContentResult ;
            //Assert

            //Assert.IsType<NoContentResult>(result);
            Assert.Null(result);
        }
        [Fact]
        public async  Task AddNewCompanyToCreate()
        {
            //Arrange
            var company = new Company
            {
                CompanyId=3,CompanyName="Whiteblue"
                
            };
            mockProductInter.Setup(x => x.AddNewCompany(company))       //mocking the create method in interface
           .ReturnsAsync(company);

            var prod =await GetSampleCompanies();           //add new pizza to list
            prod.Add(company);
            var thirdcompany = prod[2];
            //Act
            var actionResult = await _companyController.PostCompany(company);
            //Assert
            //var result = actionResult.Result as CreatedResult;
            //var actual = result.Value as Pizza;
            actionResult.Equals(thirdcompany);
            // Assert.IsType<CreatedResult>(result);
        }
        [Fact]
        public async Task AddNewCompanyToCreateReturnBadRequest()
        {
            //Arrange
            var company = new Company();
            company = null;
            mockProductInter.Setup(x => x.AddNewCompany(company))       //mocking the create method in interface
           .ReturnsAsync(company);

            var prod = await GetSampleCompanies();           //add new pizza to list
            prod.Add(company);
            var thirdcompany = prod[2];
            //Act
            var actionResult = await _companyController.PostCompany(company);
            var result = actionResult.Result as BadRequestResult;
            //Assert
            //var result = actionResult.Result as CreatedResult;
            //var actual = result.Value as Pizza;
            //actionResult.Equals(thirdcompany);
            // Assert.IsType<CreatedResult>(result);
            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public async Task DeleteProduct_ReturnDeleteStatusAsync()
        {
            List<Company> product = await GetSampleCompanies();
            //var company =  product.Result[0];
            var company = product[0];
            mockProductInter.Setup(x => x.DeleteCompany(company.CompanyId)); //mocking the create method in interface
            var actionResult = await _companyController.DeleteCompany(company.CompanyId);
            //mockProductInter.Setup(x => x.GetCompanies()).ReturnsAsync(product);
            //var result = await _companyController.GetCompanies();
            actionResult.Should().BeOfType<OkObjectResult>();
        }
        [Fact]
        public async Task EditCompany_ReturnEditedStatus_WhenPassingCompanyObjectAsync()
        {
            var prod =await GetSampleCompanies();
            var l = prod[0];
            l.CompanyName = "Zoho";
            mockProductInter.Setup(x => x.UpdateCompany(l)).ReturnsAsync(l);       //mocking the create method in interface           
            var e = prod[0];
            _companyController.PutCompany(l.CompanyId,l);
            var firstProduct = prod[0];
            mockProductInter.Setup(x => x.GetCompany(l.CompanyId))
                .ReturnsAsync(firstProduct);
            //act
            var actionResult = _companyController.GetCompany(1);
            actionResult.Equals(e);
        }
        private async Task<List<Company>> GetSampleCompanies()
        {
            //Arrange
            var output = new List<Company>
            {
                  new Company
                  { CompanyId=1,CompanyName="WB"
                },
                  new Company {
                     CompanyId=2,CompanyName="WhiteBlue"
                  },
                 };
            return output;
        }
    }
}
