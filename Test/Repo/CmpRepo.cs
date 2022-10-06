using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using PlacementApplicationNew.Model;
using PlacementApplicationNew.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacementApplicationNew.Test.Repo
{
    public class CmpRepo
    {
        //int temp = 100;
        public async Task<PlacementAppContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<PlacementAppContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
           .Options;
            var databaseContext = new PlacementAppContext(options);
            databaseContext.Database.EnsureCreated();
            int temp = 100;
            if (await databaseContext.Companies.CountAsync() <= 0)
            {

                databaseContext.Companies.Add(
                new Company()
                {
                    CompanyId = temp++,
                    CompanyName = "WB"
                });
                    databaseContext.Companies.Add(
                    new Company()
                    {
                        CompanyId = temp++,
                        CompanyName = "WhiteBlue"
                    }

                     );

                    await databaseContext.SaveChangesAsync();

                
            }
            return databaseContext;
        }

            [Fact]
        public async Task CompanyRepo_GetAllProducts_ReturnCustomers()
        {
            //Arrange
            var dbContext = await GetDatabaseContext(); //This one calls the inmemory database
            ICompany companysRepository = new CompanyRepo(dbContext); //repo layer object calling
            //Act
            var result = companysRepository.GetCompanies(); //calling the methods of repository
                                                    //Assert
            var i = dbContext.Companies.Count();
            var count = result.Result.Count();
            //result.Should().HaveCount(i);
            Assert.Equal(i,count);
            result.Should().NotBeNull();
        }



        [Fact]
        public async Task CompanyRepo_AddCompany_ReturnCompany()
        {
            //Arrange
            var p = new Company()
            {
                
                    CompanyId = 103,
                    CompanyName = "Zoho"
                
        };
            var dbContext = await GetDatabaseContext();
            ICompany prodRepository = new CompanyRepo(dbContext);
            //Act
            var result = await prodRepository.AddNewCompany(p);
            //Assert
            result.Should().BeEquivalentTo(p);
            //dbContext.Companies.Should().HaveCount(3);
            var i = dbContext.Companies.Count();
            var result1 = prodRepository.GetCompanies().Result.Count();
            Assert.Equal(i,result1);
            
        }



        [Fact]
        public async Task CompanyRepo_GetCompanyById_ReturnCompanyId()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            ICompany prodRepository = new CompanyRepo(dbContext);



            //Act
            var result = prodRepository.GetCompany(101);
            //Assert
            var name = "WhiteBlue";
            name.Should().Be(result.Result.CompanyName);



        }



        [Fact]
        public async Task CompanyRepo_UpdateCompany_ReturnEdit()
        {
            //Arrange
            var id = 101;
            var prod = new Company()
            {
                CompanyId = id,
                CompanyName = "WhiteBlue Cloud Services"
                
            };
            var dbContext = await GetDatabaseContext();
            ICompany customersRepository = new CompanyRepo(dbContext);



            //Act
            //---------------------------------------------------------
            var prodfind =await dbContext.Companies.FindAsync(prod.CompanyId);
            dbContext.Entry<Company>(prodfind).State = EntityState.Detached;//has to be used only on xUnittesting
            //----------------------------------------------------------
            var result = await customersRepository.UpdateCompany(prod);
            //Assert
            result.Should().BeEquivalentTo(prod);
            dbContext.Companies.Should().HaveCount(2);




        }



        [Fact]
        public async Task CompanyRepo_DeleteProduct()
        {
            var id = 101;
            var prod = new Company()
            {
                CompanyId = id,
                CompanyName = "WhiteBlue"
            };

            var dbContext = await GetDatabaseContext();
            ICompany dRepository = new CompanyRepo(dbContext);
            dRepository.DeleteCompany(id);
            var a =dRepository.GetCompany(id);
            a.Result.Should().BeNull();
        }
    }
}
