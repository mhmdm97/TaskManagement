namespace TaskManagementApi.Models.Responses
{
    public class PaginationResponse
    {
        public Pagination? Pagination { get; set; } = null;
    }
    public class Pagination
    {
        public Pagination(int pageSize, int pageIndex, int totalCount)
        {
            Page = pageIndex;
            PageSize = pageSize;
            PageCount = (totalCount + pageSize - 1) / pageSize;
            TotalCount = totalCount;
        }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int TotalCount { get; set; }
    }
}
