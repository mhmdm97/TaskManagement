using TaskManagementApi.DAL.EF;
using TaskManagementApi.Helpers;
using Task = TaskManagementApi.DAL.EF.Task;

namespace TaskManagementApi.Models.Dtos
{
    public class TaskDto
    {
        public TaskDto() { }
        public TaskDto(Task task)
        {
            Id = task.Id;
            Title = task.Title;
            Description = task.Description;
            DueDate = task.DueDate;
            AssignedUser = task.AssignedUser;
            TaskGroup = task.TaskGroup;
            AssignedUserName = task.AssignedUserNavigation?.DisplayName;
            TaskGroupTitle = task.TaskGroupNavigation?.Status != (int)Enums.TaskState.Deleted ? task.TaskGroupNavigation?.Title : null;
            Status = task.Status is not null ? ((Enums.TaskState)task.Status).ToString() : null;
        }
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? Status { get; set; }
        public DateTime? DueDate { get; set; }
        public int? AssignedUser { get; set; }
        public string? AssignedUserName { get; set; }
        public int? TaskGroup { get; set; }
        public string? TaskGroupTitle { get; set; }
    }
}
