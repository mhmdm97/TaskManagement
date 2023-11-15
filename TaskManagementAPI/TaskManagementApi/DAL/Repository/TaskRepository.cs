using Microsoft.EntityFrameworkCore;
using TaskManagementApi.DAL.EF;
using TaskManagementApi.DAL.IRepository;
using TaskManagementApi.Helpers;
using TaskManagementApi.Models.Filters;

namespace TaskManagementApi.DAL.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskManagementContext _context;
        private readonly ILogger<TaskRepository> _logger;
        public TaskRepository(TaskManagementContext context, ILogger<TaskRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> AddTaskAsync(EF.Task task)
        {
            try
            {
                await _context.Tasks.AddAsync(task);
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return false;
        }
        public async Task<List<EF.Task>?> GetTasksAsync(int take, int skip, TaskFilter? filter = null, int? sortProperty = null, bool sortAscending = false)
        {
            try
            {
                var pred = _context.Tasks.Where(t => t.Status != (int)Enums.TaskState.Deleted);
                if (filter is not null)
                    pred = FilterTaskQueryable(pred, filter);

                if (sortProperty is not null)
                    pred = SortTaskQueryable(pred, (int)sortProperty, sortAscending);

                return await pred.Skip(skip).Take(take)
                    .Include(t => t.TaskGroupNavigation)
                    .Include(t => t.AssignedUserNavigation)
                    .Include(t => t.CreatedByNavigation)
                    .ToListAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }
        public async Task<bool> UpdateTaskAsync(EF.Task task)
        {
            try
            {
                _context.Tasks.Update(task);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return false;
        }
        public async Task<EF.Task?> GetTaskAsync(int id)
        {
            try
            {
                return await _context.Tasks.Where(t => t.Id == id && t.Status != (int)Enums.TaskState.Deleted).FirstOrDefaultAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }
        public async Task<int?> GetTasksCountAsync(TaskFilter? filter = null)
        {
            try
            {
                var pred = _context.Tasks.Where(t => t.Status != (int)Enums.TaskState.Deleted);
                if (filter is not null)
                    pred = FilterTaskQueryable(pred, filter);

                return await pred.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        //private methods
        private static IQueryable<EF.Task> FilterTaskQueryable(IQueryable<EF.Task> pred, TaskFilter filter)
        {
            var filteredPred = pred;

            if (!string.IsNullOrEmpty(filter.SearchQuery))
            {
                var dynamicPred = PredicateBuilder.False<EF.Task>();
                var keywords = HelperMethods.SplitStringToWordList(filter.SearchQuery);
                foreach (var keyword in keywords)
                    dynamicPred = dynamicPred.Or(dp => dp.Title.Contains(keyword))
                        .Or(dp => dp.Description.Contains(keyword))
                        .Or(dp => dp.AssignedUserNavigation.DisplayName.Contains(keyword))
                        .Or(dp => dp.CreatedByNavigation.DisplayName.Contains(keyword))
                        .Or(dp => dp.TaskGroupNavigation.Title.Contains(keyword))
                        .Or(dp => dp.TaskGroupNavigation.Description.Contains(keyword));
                filteredPred = filteredPred.Where(dynamicPred); 
            }

            if (filter.Status is not null)
                filteredPred.Where(fp => fp.Status == filter.Status);
            if (filter.CreatedBy is not null)
                filteredPred.Where(fp => fp.CreatedBy == filter.CreatedBy);
            if (filter.AssignedTo is not null)
                filteredPred.Where(fp => fp.AssignedUser == filter.AssignedTo);
            if (filter.GroupId is not null)
                filteredPred.Where(fp => fp.TaskGroup == filter.GroupId);

            return filteredPred;
        }
        private static IQueryable<EF.Task> SortTaskQueryable(IQueryable<EF.Task> pred, int sortProperty, bool sortAscending)
        {
            var sortedPred = pred;
            switch (sortProperty)
            {
                case (int)Enums.TaskSortField.Title:
                    sortedPred = sortAscending ? sortedPred.OrderBy(t => t.Title) : sortedPred.OrderByDescending(t => t.Title); break;
                case (int)Enums.TaskSortField.CreationDate:
                    sortedPred = sortAscending ? sortedPred.OrderBy(t => t.CreatedOn) : sortedPred.OrderByDescending(t => t.CreatedOn); break;
                case (int)Enums.TaskSortField.DueDate:
                    sortedPred = sortAscending ? sortedPred.OrderBy(t => t.DueDate) : sortedPred.OrderByDescending(t => t.DueDate); break;
                case (int)Enums.TaskSortField.Status:
                    sortedPred = sortAscending ? sortedPred.OrderBy(t => t.Status) : sortedPred.OrderByDescending(t => t.Status); break;
            }
            return sortedPred;
        }

        
    }
}
