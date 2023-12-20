namespace BrantMonitorApi.Services;

public interface ITaskService
{
    Task<Guid> PostTaskAsync();
    Task<string> GetTaskAsync(string id);
}