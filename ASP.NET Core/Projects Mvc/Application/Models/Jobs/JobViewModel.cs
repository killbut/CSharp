using Application.Models.Workers;
using Core.Entities;

namespace Application.Models.Jobs;

public class JobViewModel
{
    public int Id { get; set; }
    public string JobName { get; set; }
    public WorkerViewModel? Author { get; set; }
    public WorkerViewModel? Performer { get; set; }
    public JobStatus Status { get; set; } = JobStatus.ToDo;
    public string Description { get; set; }
    public ushort Priority { get; set; } = 0;
}