using Application.Commons.Interfaces;
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository(UserManager<AppUser> userManager) : IUserRepository
    {
        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            var appUser = await userManager.FindByIdAsync(id.ToString());

            if (appUser == null)
                return null;

            return new User(
                appUser.Id,
                appUser.Email!,
                appUser.DisplayName);
        }
    }
}
