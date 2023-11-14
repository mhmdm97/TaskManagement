using System.ComponentModel.DataAnnotations;

namespace TaskManagementApi.Models.Requests.UserRequests
{
    public class RefreshRequest
    {
        [Required]
        public string? AccessToken { get; set; }
        [Required]
        public string? RefreshToken { get; set; }
    }
}
