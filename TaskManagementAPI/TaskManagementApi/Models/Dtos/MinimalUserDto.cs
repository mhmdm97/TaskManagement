using TaskManagementApi.DAL.EF;

namespace TaskManagementApi.Models.Dtos
{
    public class MinimalUserDto
    {
        public MinimalUserDto(User user)
        {
            Id = user.Id;
            DisplayName = user.DisplayName;
        }
        public int Id { get; set; }
        public string? DisplayName { get; set; }
    }
}
