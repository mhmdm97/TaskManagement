namespace TaskManagementApi.Models.Responses.UserResponses
{
    public class LoginResponse
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public string? RefreshTokenExpiryDate { get; set; }
    }
}
