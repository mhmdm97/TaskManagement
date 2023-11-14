using BCrypt.Net;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TaskManagementApi.DAL.EF;
using TaskManagementApi.DAL.IRepository;
using TaskManagementApi.Helpers;
using TaskManagementApi.Models.Requests.UserRequests;
using TaskManagementApi.Models.Responses.UserResponses;
using TaskManagementApi.Modules.Interfaces;
using TaskManagementApi.Services.Interfaces;

namespace TaskManagementApi.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenModule _tokenModule;
        private readonly IConfiguration _configuration;
        public UserService(IUserRepository userRepository, ITokenModule tokenModule, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _tokenModule = tokenModule;
            _configuration = configuration;
        }


        public async Task<bool> RegisterUser(RegisterRequest request)
        {
            var user = new User();
            user.EmailAddress = request.EmailAddress;
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
            user.DisplayName = request.DisplayName;

            return await _userRepository.CreateUserAsync(user);
        }

        public async Task<LoginResponse?> RefreshToken(RefreshRequest request)
        {
            string? accessToken = request.AccessToken;
            string? refreshToken = request.RefreshToken;
            //fetch username from expired token
            var principal = _tokenModule.GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
                return null;

            string? username = principal?.Identity?.Name;
            if (username == null)
                return null;

            //fetch user by username
            var user = await _userRepository.GetUserAsync(username);

            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiry <= DateTime.UtcNow)
                return null;

            var newAccessToken = _tokenModule.CreateToken(principal.Claims.ToList());
            var newRefreshToken = _tokenModule.GenerateRefreshToken();
            _ = int.TryParse(_configuration[Constants.Config_RefreshTokenExpiry], out int refreshTokenValidityInMinutes);

            user.UpdatedOn = DateTime.Now;
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.SpecifyKind(DateTime.Now.AddMinutes(refreshTokenValidityInMinutes), DateTimeKind.Unspecified);
            await _userRepository.UpdateUserAsync(user);

            return new()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken,
                RefreshTokenExpiryDate = user.RefreshTokenExpiry.Value.ToString("yyyy-MM-ddTHH:mm:ss")
            };
        }

        public async Task<LoginResponse?> Login(LoginRequest request)
        {
            //fetch user by username and validate existance
            var user = await _userRepository.GetUserAsync(request.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                return null;

            //generate access token (include username in token)
            var authClaims = new List<Claim> // set claims to be included in token
                {
                    new Claim(ClaimTypes.Name, user.EmailAddress),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
            var token = _tokenModule.CreateToken(authClaims); // generate auth token
                                                              //generate refresh token and store in DB
            var refreshToken = _tokenModule.GenerateRefreshToken();
            _ = int.TryParse(_configuration[Constants.Config_RefreshTokenExpiry], out int refreshTokenValidityInMinutes);

            user.UpdatedOn = DateTime.Now;
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.SpecifyKind(DateTime.Now.AddMinutes(refreshTokenValidityInMinutes), DateTimeKind.Unspecified);
            await _userRepository.UpdateUserAsync(user);

            //send tokens
            return new()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                RefreshTokenExpiryDate = user?.RefreshTokenExpiry.Value.ToString("yyyy-MM-ddTHH:mm:ss")
            };
        }
    }
}
