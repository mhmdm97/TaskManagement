namespace TaskManagementApi.Models.Filters
{
    public class GroupFilter
    {
        public string? SearchQuery { get; set; } = null;
        public int? CreatedBy { get; set; } = null;
    }
}
