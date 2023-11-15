using TaskManagementApi.DAL.IRepository;
using TaskManagementApi.Helpers;
using TaskManagementApi.Models.Dtos;
using TaskManagementApi.Models.Requests.TaskRequests;
using TaskManagementApi.Models.Responses;
using TaskManagementApi.Models.Responses.TaskResponses;
using TaskManagementApi.Services.Interfaces;

namespace TaskManagementApi.Services.Implementation
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<bool> CompleteTaskAsync(int id)
        {
            var task = await _taskRepository.GetTaskAsync(id);
            if (task is null)
                return false;
            task.Status = (int)Enums.TaskState.Done;
            return await _taskRepository.UpdateTaskAsync(task);
        }
        public async Task<bool> CreateTaskAsync(AddTaskRequest request)
        {
            var task = new DAL.EF.Task();
            task.Title = request.Title;
            task.Description = request.Description;
            task.DueDate = request.DueDate;
            task.AssignedUser = request.AssignedUser;
            task.TaskGroup = request.TaskGroup;
            task.Status = (int)Enums.TaskState.Active;

            return await _taskRepository.AddTaskAsync(task);
        }
        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await _taskRepository.GetTaskAsync(id);
            if (task is null)
                return false;
            task.Status = (int)Enums.TaskState.Deleted;
            return await _taskRepository.UpdateTaskAsync(task);
        }
        public async Task<GetTasksResponse?> GetTasksResponse(GetTasksRequest request)
        {
            var taskCount = await _taskRepository.GetTasksCountAsync(request.Filter);
            if (taskCount is null)
                return null;
            if (taskCount == 0)
                return new()
                {
                    Pagination = new Pagination(request.PageSize, request.PageIndex, 0)
                };
            
            var tasks = await _taskRepository.GetTasksAsync(request.PageSize, request.PageSize * (request.PageIndex - 1), request.Filter, request.SortProperty, request.SortAscending);
            if (tasks is null)
                return null;
            
            List<TaskDto> taskDtos = new();
            foreach (var task in tasks)
                taskDtos.Add(new(task));
            return new()
            {
                Pagination = new Pagination(request.PageSize, request.PageIndex, (int)taskCount),
                Tasks = taskDtos
            };
        }
        public async Task<bool> UpdateTaskAsync(UpdateTaskRequest request)
        {
            var task = await _taskRepository.GetTaskAsync(request.Id);
            if (task is null)
                return false;

            task.Title = request.Title;
            task.Description = request.Description;
            task.DueDate = request.DueDate;
            task.AssignedUser = request.AssignedUser;
            task.TaskGroup = request.TaskGroup;
            task.LastUpdated = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);

            return await _taskRepository.UpdateTaskAsync(task);
        }
    }
}
