using System.ComponentModel.DataAnnotations;
using TaskManagementApi.Models.Requests.UserRequests;

namespace TaskManagementApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterUser(RegisterRequest request);
        
    }
}
