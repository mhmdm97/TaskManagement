using System.ComponentModel.DataAnnotations;
using TaskManagementApi.DAL.EF;

namespace TaskManagementApi.Models.Requests.TaskRequests
{
    public class AddTaskRequest
    {
        [Required]
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public int? AssignedUser { get; set; }
        public int? TaskGroup { get; set; }
    }
}
