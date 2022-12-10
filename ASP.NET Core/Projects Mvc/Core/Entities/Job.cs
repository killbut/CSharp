using Core.Entities.Base;

namespace Core.Entities;

public class Job : BaseEntity
{
    public string JobName { get; set; }
    public Worker? Author { get; set; }
    public virtual int? AuthorId { get; set; }
    public Worker? Performer { get; set; }
    public virtual int? PerformerId { get; set; }
    public JobStatus Status { get; set; } = JobStatus.ToDo;
    public string Description { get; set; }
    public ushort Priority { get; set; } = 0;
}

public enum JobStatus
{
    ToDo,
    InProgress,
    Done
}