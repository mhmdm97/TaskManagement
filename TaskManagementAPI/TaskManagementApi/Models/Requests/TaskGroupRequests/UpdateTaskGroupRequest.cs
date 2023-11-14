using System.ComponentModel.DataAnnotations;

namespace TaskManagementApi.Models.Requests.TaskGroupRequests
{
    public class UpdateTaskGroupRequest : AddTaskGroupRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
