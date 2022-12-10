using Application.Models.Jobs;
using Application.Models.Workers;

namespace Application.Models.Projects
{
    public class ProjectViewModel
    {
        public int Id { get; set; }
        public string ProjectName { get;  set; }
        public string CompanyCustomer { get;  set; }
        public string CompanyExecutor { get;  set; }
        public ICollection<WorkerViewModel>? Workers { get; set; }
        public WorkerViewModel? Manager { get; set; }
        public DateTime StartTime { get;  set; }
        public DateTime EndTime { get;  set; }
        public ushort Priority { get;  set; }
        public ICollection<JobViewModel>? Jobs { get; set; }
    }
}
