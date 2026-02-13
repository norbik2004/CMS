using Application.Commons.Interfaces;
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Domain.Exceptions;
using AutoMapper;

namespace Infrastructure.Repositories
{
    public class UserRepository(UserManager<AppUser> userManager, IMapper mapper) : IUserRepository
    {
        public async Task AddAsync(User request)
        {
            if (request == null)
                throw new BadRequestException(nameof(request));

            var appUser = mapper.Map<AppUser>(request);

            await userManager.CreateAsync(appUser);
        }

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
