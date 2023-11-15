using TaskManagementApi.DAL.EF;
using TaskManagementApi.Models.Dtos;

namespace TaskManagementApi.DAL.IRepository
{
    public interface IUserRepository
    {
        Task<bool> CreateUserAsync(User user);
        Task<bool> UpdateUserAsync(User user);
        Task<User?> GetUserAsync(string email);
        Task<List<MinimalUserDto>?> GetUsersAsync();
    }
}
