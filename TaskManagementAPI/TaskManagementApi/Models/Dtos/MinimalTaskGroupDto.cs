using TaskManagementApi.DAL.EF;

namespace TaskManagementApi.Models.Dtos
{
    public class MinimalTaskGroupDto
    {
        public MinimalTaskGroupDto() { }
        public MinimalTaskGroupDto(TaskGroup taskGroup)
        {
            Id = taskGroup.Id;
            Title = taskGroup.Title;
        }
        public int Id { get; set; }
        public string? Title { get; set; }
    }
}
