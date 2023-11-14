using TaskManagementApi.Models.Dtos;
using TaskManagementApi.Models.Requests.TaskRequests;
using TaskManagementApi.Models.Responses.TaskResponses;

namespace TaskManagementApi.Services.Interfaces
{
    public interface ITaskService
    {
        Task<bool> CreateTaskAsync(AddTaskRequest request);
        Task<bool> UpdateTaskAsync(UpdateTaskRequest request);
        Task<bool> DeleteTaskAsync(int id);
        Task<bool> CompleteTaskAsync(int id);
        Task<GetTasksResponse?> GetTasksResponse(GetTasksRequest request);
    }
}
