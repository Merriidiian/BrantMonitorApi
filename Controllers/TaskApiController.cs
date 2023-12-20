using BrantMonitorApi.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace BrantMonitorApi.Controllers;


[ApiController]
[ApiVersion("1.0")]
[Route("brant-monitor/api/v{version:apiVersion}")]
public class TaskApiController : ControllerBase
{
    private readonly ITaskService _service;
    public TaskApiController(ITaskService service)
    {
        _service = service;
    }
    [Route("task")]
    [HttpPost]
    [Produces("application/json")]
    public async Task<IActionResult> PostTask()
    {
        try
        {
            var task = await _service.PostTaskAsync();
            return Accepted(task);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
    [Route("task/{id}")]
    [HttpGet]
    [Produces("text/json")]
    public async Task<IActionResult> GetTask(string id)
    {
        try
        {
            var taskStatus = await _service.GetTaskAsync(id);
            if (taskStatus == "400")
            {
                return BadRequest();
            }
            return Ok(taskStatus);
        }
        catch (Exception e)
        {
            return NotFound("GUID is not found");
        }
    } 
}