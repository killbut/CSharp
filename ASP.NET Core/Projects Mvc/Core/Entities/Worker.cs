using Core.Entities.Base;

namespace Core.Entities
{
    public class Worker : BaseEntity
    {
        public string FirstName { get;  set; }
        public string LastName { get;  set; }
        public string Patronymic { get;  set; }
        public string Email { get; set; }
        public ICollection<Project>? Projects { get; set; } = new HashSet<Project>();
        public ICollection<Job>? Jobs { get; set; }
    }
}
