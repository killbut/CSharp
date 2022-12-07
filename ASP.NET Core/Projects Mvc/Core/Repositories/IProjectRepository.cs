using Core.Entities;
using Core.Repositories.Base;

namespace Core.Repositories
{
    public interface IProjectRepository : IRepository<Project>
    {
        Project AddWorkersToProject(Project project,IEnumerable<int>? selectedWorkers,int? managerId);
        void Delete(int id);
        Project GetById(int id);
    }
}
