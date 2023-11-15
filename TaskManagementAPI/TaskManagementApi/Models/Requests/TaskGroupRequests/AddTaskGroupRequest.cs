using System.ComponentModel.DataAnnotations;

namespace TaskManagementApi.Models.Requests.TaskGroupRequests
{
    public class AddTaskGroupRequest
    {
        [Required]
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
    }
}
