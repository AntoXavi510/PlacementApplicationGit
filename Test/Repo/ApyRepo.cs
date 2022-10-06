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
    public class ApyRepo
    {
        public async Task<PlacementAppContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<PlacementAppContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
           .Options;
            var databaseContext = new PlacementAppContext(options);
            databaseContext.Database.EnsureCreated();
            int temp = 100;
            if (await databaseContext.Applys.CountAsync() <= 0)
            {

                databaseContext.Applys.Add(
               new Apply
               {
                   Id=temp++,
                   StudentId=1001,
                   RoleId=101
               }
               );
                databaseContext.Applys.Add(
                new Apply
                {
                    Id = temp++,
                    StudentId = 1001,
                    RoleId = 103
                }
                 );
                databaseContext.Applys.Add(
                new Apply
                {
                    Id = temp++,
                    StudentId = 1001,
                    RoleId = 108
                }
                 );
                databaseContext.Applys.Add(
                new Apply
                {
                    Id = temp++,
                    StudentId = 1002,
                    RoleId = 103
                }
                 );
                await databaseContext.SaveChangesAsync();
            }
            return databaseContext;
        }
        [Fact]
        public async Task ApplyRepo_GetAllApplys_ReturnApplys()
        {
            //Arrange
            var dbContext = await GetDatabaseContext(); //This one calls the inmemory database
            IApply applyRepository = new ApplyRepo(dbContext); //repo layer object calling
            //Act
            var result = await applyRepository.GetApplys(); //calling the methods of repository
                                                           //Assert
            var i = dbContext.Applys.Count();
            var count = result.Count();
            //result.Should().HaveCount(i);
            Assert.Equal(i, count);
            result.Should().NotBeNull();
        }
        [Fact]
        public async Task ApplyRepo_GetAllApplysForStudents_ReturnApplys()
        {
            //Arrange
            var dbContext = await GetDatabaseContext(); //This one calls the inmemory database
            IApply rolesRepository = new ApplyRepo(dbContext); //repo layer object calling
            var id = 1001;
            //List<Apply> result = await dbContext.Applys.Include(x => x.Student).Include(x => x.Role).ThenInclude(x => x.Company) where i.StudentId == id.ToListAsync();
            var apply = await dbContext.Applys.Include(x => x.Student).Where(y => y.StudentId == id).Include(x => x.Role).ThenInclude(x => x.Company).ToListAsync();

            //Act
            var result = await rolesRepository.GetRolesForStudent(id); //calling the methods of repository
            //Assert
            var i = apply.Count();
            var count = result.Count();
            //result.Should().HaveCount(i);
            Assert.Equal(i, count);
            result.Should().NotBeNull();
        }
        [Fact]
        public async Task ApplyRepo_GetAllApplysForRoles_ReturnApplys()
        {
            //Arrange
            var dbContext = await GetDatabaseContext(); //This one calls the inmemory database
            IApply rolesRepository = new ApplyRepo(dbContext); //repo layer object calling
            var id = 103;
            //List<Apply> result = await dbContext.Applys.Include(x => x.Student).Include(x => x.Role).ThenInclude(x => x.Company) where i.StudentId == id.ToListAsync();
            var apply = await dbContext.Applys.Include(x => x.Student).Include(x => x.Role).ThenInclude(x => x.Company).Where(y => y.RoleId == id).ToListAsync();

            //Act
            var result = await rolesRepository.GetApplyForRoles(id); //calling the methods of repository
            //Assert
            var i = apply.Count();
            var count = result.Count();
            //result.Should().HaveCount(i);
            Assert.Equal(i, count);
            result.Should().NotBeNull();
        }
        [Fact]
        public async Task ApplyRepo_Apply_ReturnApply()
        {
            //Arrange
            var p = new Apply()
            {
                Id = 104,
                StudentId = 1001,
                RoleId = 104

            };
            var dbContext = await GetDatabaseContext();
            IApply prodRepository = new ApplyRepo(dbContext);
            //Act
            var result = await prodRepository.Apply(p);
            //Assert
            result.Should().BeEquivalentTo(p);
            //dbContext.Companies.Should().HaveCount(3);
            var i = dbContext.Applys.Count();
            var result1 = prodRepository.GetApplys().Result.Count();
            Assert.Equal(i, result1);

        }
        [Fact]
        public async Task ApplyRepo_GetApplyById_ReturnApplyById()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            IApply applyRepository = new ApplyRepo(dbContext);
            //Act
            var result = applyRepository.GetApply(103);
            //Assert
            var Studentid = 1002;
            Studentid.Should().Be(result.Result.StudentId);

        }
        //        [Fact]
        //        public async Task ApplyRepo_UpdateApply_ReturnEdit()
        //        {
        //            Arrange
        //            var id = 101;
        //            var prod = new Apply()
        //            {
        //                Id = 101,
        //                StudentId = 1001,
        //                RoleId = 104


        //            };
        //            var dbContext = await GetDatabaseContext();
        //            IApply customersRepository = new ApplyRepo(dbContext);



        //            Act
        //            -------------------------------------------------------- -
        //            var prodfind = await dbContext.Applys.FindAsync(prod.Id);
        //            dbContext.Entry<Apply>(prodfind).State = EntityState.Detached;//has to be used only on xUnittesting
        //            ----------------------------------------------------------
        //var result = await customersRepository.U;
        //            Assert

        //result.Should().BeEquivalentTo(prod);
        //            var i = dbContext.Roles.Count();
        //            var result1 = customersRepository.GetRoles().Result.Count();
        //            result1.Should().Be(i);
        //        }
        [Fact]
        public async Task ApplyRepo_DeleteApply()
        {
            var id = 101;
            //var prod = new Role()
            //{
            //    CompanyId = id,
            //    CompanyName = "WhiteBlue"
            //};

            var dbContext = await GetDatabaseContext();
            IApply dRepository = new ApplyRepo(dbContext);
            dRepository.DeleteApply(id);
            var a = dRepository.GetApply(id);
            a.Result.Should().BeNull();
            var i = dbContext.Applys.Count();
            var result1 = dRepository.GetApplys().Result.Count();
            result1.Should().Be(i);
        }
    }
}
