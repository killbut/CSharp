using Core.Entities;

namespace Application.Models.Workers
{
    public class WorkerViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public ICollection<Project> Projects { get; set; }
        public ICollection<Job>? Jobs { get; set; }
        public string FullName => $"{LastName} {FirstName} {Patronymic}";
    }
}
