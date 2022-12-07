using Core.Entities.Base;

namespace Core.Entities
{
    public class Worker : BaseEntity
    {
        public string FirstName { get;  set; }
        public string LastName { get;  set; }
        public string Patronymic { get;  set; }
        public string Email { get; set; }
        public virtual ICollection<Project>? Projects { get; set; } = new HashSet<Project>();
        public virtual Project? ManageProject { get; set; }
        public Worker()
        {
            
        }
        public Worker(int id,string firstName,string lastName,string patronymic,string email, ICollection<Project> projects)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Patronymic = patronymic;
            Email = email;
            Projects = projects;
        }
    }
}
