namespace OnlineStore.Models;

public class FailedTask
{
    public int Id { get; set; } // primary key
    public string TaskName { get; set; } = string.Empty; // name of the task
    public string? Details { get; set; } // optional details about the failure
    public DateTime FailedAt { get; set; } = DateTime.UtcNow; // when it failed
    public string? ExceptionMessage { get; set; } // exception message
    public string? StackTrace { get; set; } // stack trace
    public int Try { get; set; }
        
}