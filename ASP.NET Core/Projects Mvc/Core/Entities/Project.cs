using Core.Entities.Base;

namespace Core.Entities
{
    public class Project : BaseEntity
    {
        public string ProjectName { get; set; }
        public string CompanyCustomer { get; set; }
        public string CompanyExecutor { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public ushort Priority { get; set; }
        
        public ICollection<Worker>? Workers { get; set; } = new HashSet<Worker>();
        public Worker? Manager { get; set; }
        public ICollection<Job>? Jobs { get; set; }
    }
}
