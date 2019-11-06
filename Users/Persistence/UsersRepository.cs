using System;
using System.Threading.Tasks;
using Users.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Users.Persistence
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IUsersDbContext _dbContext;

        public UsersRepository(IUsersDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<User> FindByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}