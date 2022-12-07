using System.Linq.Expressions;
using Core.Entities;
using Core.Repositories;
using Infrastructure.Context;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProjectRepository : BaseRepositories<Project>, IProjectRepository
    {
        public ProjectRepository(DataDbContext context) : base(context) { }
        
        public Project AddWorkersToProject(Project project, IEnumerable<int>? selectedWorkers, int? managerId)
        {
            if (selectedWorkers != null)
            {
                var workers=_context.Workers.Include(x=>x.Projects).Where(x => selectedWorkers.Contains(x.Id)).ToArray();
                project.Workers = workers;
            }
            if (managerId.HasValue)
            {
                project.Manager = _context.Workers.First(x => x.Id == managerId.Value);
            }

            var a = _context.ChangeTracker.Entries<Worker>().FirstOrDefault().Properties;
            foreach (var b in a)
            {
                if (b.Metadata.IsShadowProperty())
                {
                    var c = b;
                }
            }
            return project;
        }

        public void Delete(int id)
        {
            var project = _context.Projects
                .Include(x=>x.Workers)
                .Include(x => x.Manager).First(x => x.Id == id);
            Remove(project);
        }

        public Project GetById(int id)
        {
            return _context.Projects.Include(x => x.Manager)
                                    .Include(x => x.Workers)
                                    .First(x => x.Id == id);
        }
    }
}
