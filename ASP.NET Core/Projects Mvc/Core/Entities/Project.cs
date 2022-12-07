using Core.Entities.Base;

namespace Core.Entities
{
    public class Project : BaseEntity
    {
        public string ProjectName { get; set; }
        public string CompanyCustomer { get; set; }
        public string CompanyExecutor { get; set; }
        public virtual ICollection<Worker>? Workers { get; set; } = new HashSet<Worker>();
        public virtual Worker? Manager { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public ushort Priority { get; set; }

        public Project()
        {
                
        }

        public Project(int id,string projectName,string companyCustomer,string companyExecutor,ICollection<Worker> workers, Worker manager, DateTime start, DateTime end,ushort priority)
        {
            Id = id;
            ProjectName = projectName;
            CompanyCustomer = companyCustomer;
            CompanyExecutor = companyExecutor;
            Workers = workers;
            Manager = manager;
            StartTime = start;
            EndTime = end;
            Priority = priority;
        }
    }
}
