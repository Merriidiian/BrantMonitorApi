using BrantMonitorApi.Models;
using BrantMonitorApi.Repositories;

namespace BrantMonitorApi.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;
    private readonly ILogger _logger;

    public TaskService(ITaskRepository repository, ILogger<TaskService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Guid> PostTaskAsync()
    {
        var newTask = new TaskModel
        {
            Id = new Guid(),
            DataTimeTask = DateTimeOffset.Now,
            Status = "created"
        };
        _logger.LogInformation($"New task is created : {Newtonsoft.Json.JsonConvert.SerializeObject(newTask)}");
        var insertDbResult = await _repository.PostTaskAsync(newTask);
        if (insertDbResult)
        {
            _logger.LogInformation($"Task {newTask.Id} update is delay 2 min");
            var token = new CancellationToken();
            Task.Run(() => RunningTaskAsync(newTask.Id),token);
            Task.Run(() => UpdateTaskStatusAsync(newTask.Id),token);
            return newTask.Id;
        }
        else
        {
            throw new Exception("TaskService error");
        }
    }

    public async Task<string> GetTaskAsync(string id)
    {
        var guidId = Guid.Empty;
        var guidIsValid = Guid.TryParse(id, out guidId);
        if (guidIsValid)
        {
            _logger.LogInformation("Guid is valid");
            return await _repository.GetTaskAsync(guidId);
        }
        else
        {
            _logger.LogInformation("Guid is not valid");
            return "400";
        }
    }

    private async Task UpdateTaskStatusAsync(Guid id)
    {
        await Task.Delay(120000);
        await _repository.UpdateTaskStatusFinishAsync(id);
    }

    private async Task RunningTaskAsync(Guid id)
    {
        await _repository.UpdateTaskStatusRunningAsync(id);
    }
}