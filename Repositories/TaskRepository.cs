using BrantMonitorApi.DataContext;
using BrantMonitorApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BrantMonitorApi.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly TaskContext _context;
    private readonly ILogger _logger;

    public TaskRepository(TaskContext context, ILogger<TaskRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> PostTask(TaskModel taskModel)
    {
        try
        {
            _context.TaskModel.Add(taskModel);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Add new task to DB: {Newtonsoft.Json.JsonConvert.SerializeObject(taskModel)}",
                DateTime.UtcNow.ToLongTimeString());
            return true;
        }
        catch (Exception e)
        {
            _logger.LogInformation($"Error: {e.StackTrace}", DateTime.UtcNow.ToLongTimeString());
            return false;
        }
    }

    public async Task<string?> GetTask(Guid id)
    {
        return _context.TaskModel.Find(id).Status;
    }

    public async Task<bool> UpdateTaskStatus(Guid id)
    {
        try
        {
            await using var thisContext = new TaskContext(new DbContextOptions<TaskContext>());
            var updateTask = await thisContext.TaskModel.FindAsync(id);
            updateTask.Status = "finished";
            updateTask.DataTimeTask = DateTimeOffset.Now;
            await thisContext.SaveChangesAsync();
            _logger.LogInformation($"Task {updateTask.Id} is updated");
            await thisContext.DisposeAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogInformation($"Update task is error: {e.StackTrace}");
            return false;
        }
    }
}