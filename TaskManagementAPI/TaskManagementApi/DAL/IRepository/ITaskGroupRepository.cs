using TaskManagementApi.DAL.EF;
using TaskManagementApi.Models.Dtos;
using TaskManagementApi.Models.Filters;

namespace TaskManagementApi.DAL.IRepository
{
    public interface ITaskGroupRepository
    {
        Task<bool> AddTaskGroupAsync(TaskGroup taskGroup);
        Task<bool> UpdateTaskGroupAsync(TaskGroup taskGroup);
        Task<List<TaskGroup>?> GetTaskGroupsAsync(int take, int skip, GroupFilter? filter = null, int? sortProperty = null, bool sortAscending = false);
        Task<int?> GetTaskGroupsCountAsync(GroupFilter? filter);
        Task<List<MinimalTaskGroupDto>?> GetAllTaskGroupsAsync();
        Task<TaskGroup?> GetTaskGroupAsync(int id);

    }
}
