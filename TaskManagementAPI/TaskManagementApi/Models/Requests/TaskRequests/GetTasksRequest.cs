using System.ComponentModel.DataAnnotations;
using TaskManagementApi.Models.Filters;

namespace TaskManagementApi.Models.Requests.TaskRequests
{
    public class GetTasksRequest
    {
        [Range(1, int.MaxValue)]
        public int PageSize { get; set; } = 20;
        [Range(1, int.MaxValue)]
        public int PageIndex { get; set; } = 1;
        public TaskFilter? Filter { get; set; }
        public int? SortProperty { get; set; } = null;
        public bool SortAscending { get; set; } = false;

    }
}
