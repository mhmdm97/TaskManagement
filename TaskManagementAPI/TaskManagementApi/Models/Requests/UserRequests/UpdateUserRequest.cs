using System.ComponentModel.DataAnnotations;

namespace TaskManagementApi.Models.Requests.UserRequests
{
    public class UpdateUserRequest
    {
        [Required]
        public string EmailAddress { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string DisplayName { get; set; } = null!;

    }
}
