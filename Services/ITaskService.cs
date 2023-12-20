namespace BrantMonitorApi.Services;

public interface ITaskService
{
    Task<Guid> PostTask();
    Task<string> GetTask(string id);
}