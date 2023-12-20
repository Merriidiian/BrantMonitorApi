using BrantMonitorApi.Models;

namespace BrantMonitorApi.Repositories;

public interface ITaskRepository
{
    Task<bool> PostTask(TaskModel taskModel);
    Task<string?> GetTask(Guid id);
    Task<bool> UpdateTaskStatus(Guid id);
}