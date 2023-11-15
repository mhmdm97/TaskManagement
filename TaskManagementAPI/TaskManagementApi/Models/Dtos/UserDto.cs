using TaskManagementApi.DAL.EF;

namespace TaskManagementApi.Models.Dtos
{
    public class UserDto
    {
        public UserDto() { }
        public UserDto(User user)
        {
            EmailAddress = user.EmailAddress;
            DisplayName = user.DisplayName;
        }
        public string EmailAddress { get; set; }
        public string DisplayName { get; set; }
    }
}
