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
    public class AdRepo
    {
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

                databaseContext.Admins.Add(
                    new Admin
                    {
                        UserId = 1,
                        UserName = "Admin",
                        Password = "Antony@0123"
                    }

                    );
                databaseContext.Admins.Add(
                        new Admin
                        {
                            UserId = 2,
                            UserName = "Admin1",
                            Password = "Antony@0123"

                        }
                        );

                await databaseContext.SaveChangesAsync();
            }
            return databaseContext;
        }
        [Fact]
        public async Task LoginRepo_UserLogin_ReturnAdmin()
        {
            //Arrange

            var dbContext = await GetDatabaseContext();
            var admin = new Admin
            {
                UserId = 1,
                UserName = "Admin",
                Password = "Antony@0123"
            };
            IAdmin adminsRepository = new AdminRepo(dbContext);
            var result = adminsRepository.Login(admin).Result;
            admin.UserName.Should().Be(result.UserName);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task AdminRepo_AddAdmin_ReturnAdmin()
        {
            //Arrange
            var admin = new Admin()
            {
                UserId = 3,
                UserName = "Admin3",
                Password = "Antony@0123"

            };
            var dbContext = await GetDatabaseContext();
            IAdmin prodRepository = new AdminRepo(dbContext);
            //Act
            var result = await prodRepository.AddNewAdmin(admin);
            //Assert
            result.Should().BeEquivalentTo(admin);
            //dbContext.Companies.Should().HaveCount(3);
            var i = dbContext.Admins.Count();
            //var result1 = prodRepository.GetStudents().Result.Count();
            Assert.Equal(i, 3);

        }
    }
}