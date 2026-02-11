using Domain.Entities;

namespace Application.Commons.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIdAsync(Guid id);
    }
}
