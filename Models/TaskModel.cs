using System.ComponentModel.DataAnnotations;

namespace BrantMonitorApi.Models;

public class TaskModel
{
    [Key] [Required] public Guid Id { get; set; }
    [Required] [DataType(DataType.Date)] public DateTimeOffset DataTimeTask { get; set; }
    [Required] public string Status { get; set; }
}