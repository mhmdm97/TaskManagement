using System.ComponentModel.DataAnnotations;
using TaskManagementApi.Models.Filters;

namespace TaskManagementApi.Models.Requests.TaskGroupRequests
{
    public class GetTaskGroupsRequest
    {
        [Range(1, int.MaxValue)]
        public int PageSize { get; set; } = 20;
        [Range(1, int.MaxValue)]
        public int PageIndex { get; set; } = 1;
        public GroupFilter? Filter { get; set; }
        public int? SortProperty { get; set; } = null;
        public bool SortAscending { get; set; } = false;
    }
}
