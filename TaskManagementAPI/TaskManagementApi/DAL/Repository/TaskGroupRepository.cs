using Microsoft.EntityFrameworkCore;
using TaskManagementApi.DAL.EF;
using TaskManagementApi.DAL.IRepository;
using TaskManagementApi.Helpers;
using TaskManagementApi.Models.Dtos;
using TaskManagementApi.Models.Filters;

namespace TaskManagementApi.DAL.Repository
{
    public class TaskGroupRepository : ITaskGroupRepository
    {
        private readonly TaskManagementContext _context;
        private readonly ILogger<TaskGroupRepository> _logger;
        public TaskGroupRepository(TaskManagementContext context, ILogger<TaskGroupRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> AddTaskGroupAsync(TaskGroup taskGroup)
        {
            try
            {
                await _context.TaskGroups.AddAsync(taskGroup);
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return false;
        }
        public async Task<List<TaskGroup>?> GetTaskGroupsAsync(int take, int skip, GroupFilter? filter = null, int? sortProperty = null, bool sortAscending = false)
        {
            try
            {
                var pred = _context.TaskGroups.Where(t => t.Status != (int)Enums.TaskState.Deleted);
                if (filter is not null)
                    pred = FilterTaskGroupQueryable(pred, filter);

                if (sortProperty is not null)
                    pred = SortTaskGroupQueryable(pred, (int)sortProperty, sortAscending);

                return await pred.Skip(skip).Take(take)
                    .Include(t => t.CreatedByNavigation)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }
        public async Task<bool> UpdateTaskGroupAsync(TaskGroup taskGroup)
        {
            try
            {
                _context.TaskGroups.Update(taskGroup);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return false;
        }
        public async Task<List<MinimalTaskGroupDto>?> GetAllTaskGroupsAsync()
        {
            try
            {
                return await _context.TaskGroups.Select(tg => new MinimalTaskGroupDto(tg)).ToListAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }
        public async Task<TaskGroup?> GetTaskGroupAsync(int id)
        {
            try
            {
                return await _context.TaskGroups.Where(tg => tg.Id == id && tg.Status != (int)Enums.TaskState.Deleted).FirstOrDefaultAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }
        public async Task<int?> GetTaskGroupsCountAsync(GroupFilter? filter)
        {
            try
            {
                var pred = _context.TaskGroups.Where(t => t.Status != (int)Enums.TaskState.Deleted);
                if (filter is not null)
                    pred = FilterTaskGroupQueryable(pred, filter);

                return await pred.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }
        //private methods
        private static IQueryable<TaskGroup> FilterTaskGroupQueryable(IQueryable<TaskGroup> pred, GroupFilter filter)
        {
            var filteredPred = pred;

            if (!string.IsNullOrEmpty(filter.SearchQuery))
            {
                var dynamicPred = PredicateBuilder.False<TaskGroup>();
                var keywords = HelperMethods.SplitStringToWordList(filter.SearchQuery);
                foreach (var keyword in keywords)
                    dynamicPred = dynamicPred.Or(dp => dp.Title.Contains(keyword))
                        .Or(dp => dp.Description.Contains(keyword))
                        .Or(dp => dp.CreatedByNavigation.DisplayName.Contains(keyword));
                filteredPred = filteredPred.Where(dynamicPred);
            }
            if (filter.CreatedBy is not null)
                filteredPred = filteredPred.Where(fp => fp.CreatedBy == filter.CreatedBy);

            return filteredPred;
        }
        private static IQueryable<TaskGroup> SortTaskGroupQueryable(IQueryable<TaskGroup> pred, int sortProperty, bool sortAscending)
        {
            var sortedPred = pred;
            switch (sortProperty)
            {
                case (int)Enums.GroupSortField.Title:
                    sortedPred = sortAscending ? sortedPred.OrderBy(t => t.Title) : sortedPred.OrderByDescending(t => t.Title); break;
                case (int)Enums.GroupSortField.CreationDate:
                    sortedPred = sortAscending ? sortedPred.OrderBy(t => t.CreatedOn) : sortedPred.OrderByDescending(t => t.CreatedOn); break;
            }
            return sortedPred;
        }

    }
}
