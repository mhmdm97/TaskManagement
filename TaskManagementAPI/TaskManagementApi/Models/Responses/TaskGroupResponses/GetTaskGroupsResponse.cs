using TaskManagementApi.Models.Dtos;

namespace TaskManagementApi.Models.Responses.TaskGroupResponses
{
    public class GetTaskGroupsResponse : PaginationResponse
    {
        public List<TaskGroupDto> TaskGroups { get; set; } = new();
    }
}
