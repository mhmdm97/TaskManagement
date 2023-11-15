using TaskManagementApi.Models.Dtos;

namespace TaskManagementApi.Models.Responses.TaskResponses
{
    public class GetTasksResponse : PaginationResponse
    {
        public List<TaskDto> Tasks { get; set; } = new();
    }
}
