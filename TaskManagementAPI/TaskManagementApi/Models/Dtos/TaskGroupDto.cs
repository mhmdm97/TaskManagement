using TaskManagementApi.DAL.EF;

namespace TaskManagementApi.Models.Dtos
{
    public class TaskGroupDto
    {
        public TaskGroupDto() { }
        public TaskGroupDto(TaskGroup taskGroup)
        {
            Id = taskGroup.Id;
            Title = taskGroup.Title;
            Description = taskGroup.Description;
        }
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
    }
}
