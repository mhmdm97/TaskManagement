using System.ComponentModel.DataAnnotations;

namespace TaskManagementApi.Models.Requests.TaskRequests
{
    public class UpdateTaskRequest : AddTaskRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
