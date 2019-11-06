using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Users.Models;

namespace Users.Persistence
{
    public interface IUsersDbContext
    {
        DbSet<User> Users { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}