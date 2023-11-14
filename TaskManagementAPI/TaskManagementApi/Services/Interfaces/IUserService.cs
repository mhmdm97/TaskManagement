using System.ComponentModel.DataAnnotations;
using TaskManagementApi.Models.Requests.UserRequests;
using TaskManagementApi.Models.Responses.UserResponses;

namespace TaskManagementApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterUser(RegisterRequest request);
        Task<LoginResponse?> Login(LoginRequest request);
        Task<LoginResponse?> RefreshToken(RefreshRequest request);
        
    }
}
