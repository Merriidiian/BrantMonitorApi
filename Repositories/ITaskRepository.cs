using BrantMonitorApi.Models;

namespace BrantMonitorApi.Repositories;

public interface ITaskRepository
{
    Task<bool> PostTaskAsync(TaskModel taskModel);
    Task<string?> GetTaskAsync(Guid id);
    Task<bool> UpdateTaskStatusFinishAsync(Guid id);
    Task<bool> UpdateTaskStatusRunningAsync(Guid id);
}