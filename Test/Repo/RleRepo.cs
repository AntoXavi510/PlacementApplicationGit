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
    public class RleRepo
    {
        public async Task<PlacementAppContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<PlacementAppContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
           .Options;
            var databaseContext = new PlacementAppContext(options);
            databaseContext.Database.EnsureCreated();
            int temp = 100;
            if (await databaseContext.Roles.CountAsync() <= 0)
            {

                databaseContext.Roles.Add(
               new Role
               {
                   RoleId = temp++,
                   RoleName = "SoftwareDeveloper",
                   SalaryPackage = 300000,
                   CutoffPercentage = 9,
                   DateOfDrive = new DateTime(2024, 10, 10),
                   Location = "Chennai",
                   CompanyID = 1
               }
               );
                databaseContext.Roles.Add(
                new Role
                {
                    RoleId = temp++,
                    RoleName = "UI/UX Developer",
                    SalaryPackage = 300000,
                    CutoffPercentage = 9,
                    DateOfDrive = new DateTime(2024, 10, 09),
                    Location = "Chennai",
                    CompanyID = 1
                }
                 );

                await databaseContext.SaveChangesAsync();
            }
            return databaseContext;
        }
        [Fact]
        public async Task RoleRepo_GetAllRoles_ReturnRoles()
        {
            //Arrange
            var dbContext = await GetDatabaseContext(); //This one calls the inmemory database
            IRoles rolesRepository = new RoleRepo(dbContext); //repo layer object calling
            //Act
            var result =await rolesRepository.GetRoles(); //calling the methods of repository
                                                            //Assert
            var i = dbContext.Roles.Count();
            var count = result.Count();
            //result.Should().HaveCount(i);
            Assert.Equal(i, count);
            result.Should().NotBeNull();
        }
        [Fact]
        public async Task RoleRepo_GetAllRolesForCompanies_ReturnRoles()
        {
            //Arrange
            var dbContext = await GetDatabaseContext(); //This one calls the inmemory database
            IRoles rolesRepository = new RoleRepo(dbContext); //repo layer object calling
            var id = 1;
            var apply = await dbContext.Roles.Include(x => x.Company).Where(y => y.CompanyID == id).ToListAsync();
            
            //Act
            var result = await rolesRepository.GetRolesForCompany(id); //calling the methods of repository
            //Assert
            var i = apply.Count();
            var count = result.Count();
            //result.Should().HaveCount(i);
            Assert.Equal(i, count);
            result.Should().NotBeNull();
        }
        [Fact]
        public async Task RoleRepo_AddRole_ReturnRole()
        {
            //Arrange
            var p = new Role()
            {
                RoleId = 102,
                RoleName = "UI/UX Developer",
                SalaryPackage = 300000,
                CutoffPercentage = 9,
                DateOfDrive = new DateTime(2024, 10, 09),
                Location = "Chennai",
                CompanyID = 2

            };
            var dbContext = await GetDatabaseContext();
            IRoles prodRepository = new RoleRepo(dbContext);
            //Act
            var result = await prodRepository.AddNewRole(p);
            //Assert
            result.Should().BeEquivalentTo(p);
            //dbContext.Companies.Should().HaveCount(3);
            var i = dbContext.Roles.Count();
            var result1 = prodRepository.GetRoles().Result.Count();
            Assert.Equal(i, result1);

        }
        [Fact]
        public async Task RoleRepo_GetRoleById_ReturnRoleId()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            IRoles prodRepository = new RoleRepo(dbContext);



            //Act
            var result = prodRepository.GetRole(101);
            //Assert
            var name = "UI/UX Developer";
            name.Should().Be(result.Result.RoleName);

        }
        [Fact]
        public async Task RoleRepo_UpdateRole_ReturnEdit()
        {
            //Arrange
            var id = 101;
            var prod = new Role()
            {
                RoleId = id,
                RoleName = "UI/UX Developer",
                SalaryPackage = 300000,
                CutoffPercentage = 9,
                DateOfDrive = new DateTime(2024, 10, 09),
                Location = "Chennai",
                CompanyID = 1

            };
            var dbContext = await GetDatabaseContext();
            IRoles customersRepository = new RoleRepo(dbContext);



            //Act
            //---------------------------------------------------------
            var prodfind = await dbContext.Roles.FindAsync(prod.RoleId);
            dbContext.Entry<Role>(prodfind).State = EntityState.Detached;//has to be used only on xUnittesting
            //----------------------------------------------------------
            var result = await customersRepository.UpdateRole(prod);
            //Assert
            result.Should().BeEquivalentTo(prod);
           var i= dbContext.Roles.Count();
            var result1 = customersRepository.GetRoles().Result.Count();
            result1.Should().Be(i);
        }
        [Fact]
        public async Task RoleRepo_DeleteRole()
        {
            var id = 101;
            //var prod = new Role()
            //{
            //    CompanyId = id,
            //    CompanyName = "WhiteBlue"
            //};

            var dbContext = await GetDatabaseContext();
            IRoles dRepository = new RoleRepo(dbContext);
            dRepository.DeleteRole(id);
            var a = dRepository.GetRole(id);
            a.Result.Should().BeNull();
            var i = dbContext.Roles.Count();
            var result1 = dRepository.GetRoles().Result.Count();
            result1.Should().Be(i);
        }
    }
}
