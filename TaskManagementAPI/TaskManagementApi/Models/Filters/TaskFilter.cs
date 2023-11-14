namespace TaskManagementApi.Models.Filters
{
    public class TaskFilter
    {
        public string? SearchQuery { get; set; } = null;
        public int? Status { get; set; } = null;
        public int? GroupId { get; set; } = null;
        public int? CreatedBy { get; set; } = null;
        public int? AssignedTo { get; set; } = null;
    }
}
