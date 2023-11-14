using TaskManagementApi.DAL.EF;
using TaskManagementApi.DAL.IRepository;
using TaskManagementApi.Helpers;
using TaskManagementApi.Models.Dtos;
using TaskManagementApi.Models.Requests.TaskGroupRequests;
using TaskManagementApi.Models.Responses;
using TaskManagementApi.Models.Responses.TaskGroupResponses;
using TaskManagementApi.Services.Interfaces;

namespace TaskManagementApi.Services.Implementation
{
    public class TaskGroupService : ITaskGroupService
    {
        private readonly ITaskGroupRepository _taskGroupRepository;
        public TaskGroupService(ITaskGroupRepository taskGroupRepository)
        {
            _taskGroupRepository = taskGroupRepository;
        }
        public async Task<bool> CreateTaskGroupAsync(AddTaskGroupRequest request)
        {
            var taskGroup = new TaskGroup();
            taskGroup.Title = request.Title;
            taskGroup.Description = request.Description;
            taskGroup.Status = (int)Enums.TaskState.Active;
            return await _taskGroupRepository.AddTaskGroupAsync(taskGroup);
        }

        public async Task<bool> DeleteTaskGroupAsync(int id)
        {
            var taskGroup = await _taskGroupRepository.GetTaskGroupAsync(id);
            if (taskGroup is null)
                return false;
            taskGroup.Status = (int)Enums.TaskState.Deleted;
            return await _taskGroupRepository.UpdateTaskGroupAsync(taskGroup);
        }

        public async Task<GetTaskGroupsResponse?> GetTaskGroupsAsync(GetTaskGroupsRequest request)
        {
            var taskGroupsCount = await _taskGroupRepository.GetTaskGroupsCountAsync(request.Filter);
            if (taskGroupsCount is null)
                return null;
            if (taskGroupsCount == 0)
                return new()
                {
                    Pagination = new Pagination(request.PageSize, request.PageIndex, 0)
                };
            var taskGroups = await _taskGroupRepository.GetTaskGroupsAsync(request.PageSize, request.PageSize * (request.PageIndex - 1), request.Filter, request.SortProperty, request.SortAscending);
            if (taskGroups is null)
                return null;

            List<TaskGroupDto> taskGroupDtos = new();
            foreach (var taskGroup in taskGroups)
                taskGroupDtos.Add(new(taskGroup));
            return new()
            {
                Pagination = new Pagination(request.PageSize, request.PageIndex, (int)taskGroupsCount),
                TaskGroups = taskGroupDtos
            };
        }

        public async Task<bool> UpdateTaskGroupAsync(UpdateTaskGroupRequest request)
        {
            var taskGroup = await _taskGroupRepository.GetTaskGroupAsync(request.Id);
            if (taskGroup is null)
                return false;
            taskGroup.Title = request.Title;
            taskGroup.Description = request.Description;
            taskGroup.LastUpdated = DateTime.UtcNow;

            return await _taskGroupRepository.UpdateTaskGroupAsync(taskGroup);
        }
    }
}
