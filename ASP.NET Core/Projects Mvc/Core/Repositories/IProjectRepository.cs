using Core.Entities;
using Core.Repositories.Base;

namespace Core.Repositories
{
    public interface IProjectRepository : IRepository<Project>
    {
        Project AddWorkersToProject(Project project,IEnumerable<int>? selectedWorkers,int? managerId);
        Project AddJobsToProject(Project project, IEnumerable<int>? selectedJobs);
        void Delete(int id);
        Project GetById(int id);
    }
}
