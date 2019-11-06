using System;
using System.Threading;
using System.Threading.Tasks;
using Users.Models;
using Users.Persistence;
using Xunit;

namespace Users.Tests.Integration
{
    public class UsersRepositoryTests : IClassFixture<DbFixture>
    {
        private readonly DbFixture _fixture;

        public UsersRepositoryTests(DbFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task FindById_should_return_null_when_id_invalid()
        {
            var id = Guid.NewGuid();

            using(var dbContext = _fixture.BuildDbContext())
            {
                var sut = new UsersRepository(dbContext);
                var result = await sut.FindByIdAsync(id, CancellationToken.None);
                Assert.Null(result);
            }
        }

        [Fact]
        public async Task FindById_should_return_user_when_id_valid()
        {
            var user = new User(Guid.NewGuid(), "user", "by id");

            using (var dbContext = _fixture.BuildDbContext())
            {
                dbContext.Users.Add(user);
                await dbContext.SaveChangesAsync();
            }

            using (var dbContext = _fixture.BuildDbContext())
            {
                var sut = new UsersRepository(dbContext);
                var result = await sut.FindByIdAsync(user.Id, CancellationToken.None);
                Assert.NotNull(result);
                Assert.Equal(user.Id, result.Id);
                Assert.Equal(user.FirstName, result.FirstName);
                Assert.Equal(user.LastName, result.LastName);
            }
        }
    }
}
