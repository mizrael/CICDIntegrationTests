using System;
using System.Threading;
using System.Threading.Tasks;
using Users.Models;

namespace Users.Persistence
{
    public interface IUsersRepository
    {
        Task<User> FindByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}