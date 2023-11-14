using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.Models.Requests.TaskRequests;
using TaskManagementApi.Services.Interfaces;

namespace TaskManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly ILogger<TaskController> _logger;
        public TaskController(ITaskService taskService, ILogger<TaskController> logger)
        {
            _taskService = taskService;
            _logger = logger;
        }
        [HttpPost("CreateTask")]
        public async Task<IActionResult> CreateTask(AddTaskRequest request)
        {
            try
            {
                var res = await _taskService.CreateTaskAsync(request);
                if (res)
                    return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return StatusCode(500);
        }
        [HttpPut("UpdateTask")]
        public async Task<IActionResult> UpdateTask(UpdateTaskRequest request)
        {
            try
            {
                var res = await _taskService.UpdateTaskAsync(request);
                if (res)
                    return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return StatusCode(500);
        }
        [HttpDelete("DeleteTask")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            try
            {
                var res = await _taskService.DeleteTaskAsync(id);
                if (res)
                    return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return StatusCode(500);
        }
        [HttpPut("CompleteTask")]
        public async Task<IActionResult> CompleteTask(int id)
        {
            try
            {
                var res = await _taskService.CompleteTaskAsync(id);
                if (res)
                    return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return StatusCode(500);
        }
        [HttpPost("GetTasks")]
        public async Task<IActionResult> GetTasks(GetTasksRequest request)
        {
            try
            {
                var res = await _taskService.GetTasksResponse(request);
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
