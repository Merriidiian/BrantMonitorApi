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

    public async Task<Guid> PostTask()
    {
        var newTask = new TaskModel
        {
            Id = new Guid(),
            DataTimeTask = DateTimeOffset.Now,
            Status = "created"
        };
        _logger.LogInformation($"New task is created : {Newtonsoft.Json.JsonConvert.SerializeObject(newTask)}");
        var insertDbResult = await _repository.PostTask(newTask);
        if (insertDbResult)
        {
            Task.Run(() => UpdateTaskStatus(newTask.Id));
            _logger.LogInformation($"Task {newTask.Id} update is delay 2 min");
            return newTask.Id;
        }
        else
        {
            throw new Exception("TaskService error");
        }
    }

    public async Task<string> GetTask(string id)
    {
        var guidId = Guid.Empty;
        var guidIsValid = Guid.TryParse(id, out guidId);
        if (guidIsValid)
        {
            _logger.LogInformation("Guid is valid");
            return await _repository.GetTask(guidId);
        }
        else
        {
            _logger.LogInformation("Guid is not valid");
            return "400";
        }
    }

    private async Task UpdateTaskStatus(Guid id)
    {
        await Task.Delay(120000);
        await _repository.UpdateTaskStatus(id);
    }
}