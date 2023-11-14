namespace TaskManagementApi.Helpers
{
    public static class Enums
    {
        public enum TaskState
        {
            Active = 1,
            Done = 2,
            Deleted = 3
        }
        public enum TaskSortField
        {
            CreationDate = 1,
            Title = 2,
            DueDate = 3,
            Status = 4
        }
        public enum GroupSortField
        {
            CreationDate = 1,
            Title = 2
        }
    }
}
