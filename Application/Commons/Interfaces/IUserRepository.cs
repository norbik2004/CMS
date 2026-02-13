using Domain.Entities;

namespace Application.Commons.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIdAsync(Guid id);
        Task AddAsync(User request);
    }
}
