using TaskManagementApi.Models.Requests.TaskGroupRequests;
using TaskManagementApi.Models.Responses.TaskGroupResponses;

namespace TaskManagementApi.Services.Interfaces
{
    public interface ITaskGroupService
    {
        Task<bool> CreateTaskGroupAsync(AddTaskGroupRequest request);
        Task<bool> UpdateTaskGroupAsync(UpdateTaskGroupRequest request);
        Task<bool> DeleteTaskGroupAsync(int id);
        Task<GetTaskGroupsResponse?> GetTaskGroupsAsync(GetTaskGroupsRequest request);
    }
}
