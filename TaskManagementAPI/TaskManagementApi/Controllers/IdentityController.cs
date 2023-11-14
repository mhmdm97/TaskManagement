using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.Models.Requests.UserRequests;
using TaskManagementApi.Services.Interfaces;

namespace TaskManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<IdentityController> _logger;
        public IdentityController(IUserService userService, ILogger<IdentityController> logger)
        {
            _userService = userService;
            _logger = logger;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            try
            {
                var res = await _userService.RegisterUser(request);
                if (res)
                    return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return StatusCode(500);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            try
            {
                var res = await _userService.Login(request);
                if (res is not null)
                    return Ok(res);
                else
                    return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return StatusCode(500);
        }
        [HttpPost("RefreshLogin")]
        public async Task<IActionResult> RefreshLogin(RefreshRequest request)
        {
            try
            {
                var res = await _userService.RefreshToken(request);
                if (res is not null)
                    return Ok(res);
                else
                    return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return StatusCode(500);
        }
    }
}
