using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.Models.Requests.TaskGroupRequests;
using TaskManagementApi.Services.Implementation;
using TaskManagementApi.Services.Interfaces;

namespace TaskManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskGroupController : ControllerBase
    {
        private readonly ITaskGroupService _taskGroupService;
        private readonly ILogger<TaskGroupController> _logger;
        public TaskGroupController(ITaskGroupService taskGroupService, ILogger<TaskGroupController> logger)
        {
            _taskGroupService = taskGroupService;
            _logger = logger;
        }
        [HttpPost("CreateTaskGroup")]
        public async Task<IActionResult> CreateTaskGroup(AddTaskGroupRequest request)
        {
            try
            {
                var res = await _taskGroupService.CreateTaskGroupAsync(request);
                if (res)
                    return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return StatusCode(500);
        }
        [HttpPut("UpdateTaskGroup")]
        public async Task<IActionResult> UpdateTaskGroup(UpdateTaskGroupRequest request)
        {
            try
            {
                var res = await _taskGroupService.UpdateTaskGroupAsync(request);
                if (res)
                    return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return StatusCode(500);
        }
        [HttpDelete("DeleteTaskGroup")]
        public async Task<IActionResult> DeleteTaskGroup(int id)
        {
            try
            {
                var res = await _taskGroupService.DeleteTaskGroupAsync(id);
                if (res)
                    return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return StatusCode(500);
        }
        [HttpPost("GetTaskGroups")]
        public async Task<IActionResult> GetTaskGroups(GetTaskGroupsRequest request)
        {
            try
            {
                var res = await _taskGroupService.GetTaskGroupsAsync(request);
                if (res is not null)
                    return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return StatusCode(500);
        }
    }
}
