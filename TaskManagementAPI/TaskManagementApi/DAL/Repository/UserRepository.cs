using Microsoft.EntityFrameworkCore;
using TaskManagementApi.DAL.EF;
using TaskManagementApi.DAL.IRepository;
using TaskManagementApi.Models.Dtos;

namespace TaskManagementApi.DAL.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly TaskManagementContext _context;
        private readonly ILogger<UserRepository> _logger;
        public UserRepository(TaskManagementContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return false;
        }
        public async Task<User?> GetUserAsync(string email)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(u => u.EmailAddress == email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }
        public async Task<List<MinimalUserDto>?> GetUsersAsync()
        {
            try
            {
                return await _context.Users.Select(u => new MinimalUserDto(u)).ToListAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return false;
        }
    }
}
