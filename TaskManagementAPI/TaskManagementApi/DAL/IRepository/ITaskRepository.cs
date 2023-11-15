using TaskManagementApi.Models.Filters;

namespace TaskManagementApi.DAL.IRepository
{
    public interface ITaskRepository
    {
        Task<bool> AddTaskAsync(EF.Task task);
        Task<bool> UpdateTaskAsync(EF.Task task);
        Task<List<EF.Task>?> GetTasksAsync(int take, int skip, TaskFilter? filter = null, int? sortProperty = null, bool sortAscending = false);
        Task<int?> GetTasksCountAsync(TaskFilter? filter = null);
        Task<EF.Task?> GetTaskAsync(int id);
    }
}
