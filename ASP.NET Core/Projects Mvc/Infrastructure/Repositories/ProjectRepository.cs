using System.Collections.Immutable;
using Core.Entities;
using Core.Repositories;
using Infrastructure.Context;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(DataDbContext context) : base(context) { }
        
        public Project AddWorkersToProject(Project project, IEnumerable<int>? selectedWorkers, int? managerId)
        {
            if (selectedWorkers != null)
            {
                project.Workers = _context.Workers.Include(x=>x.Projects).Where(x => selectedWorkers.Contains(x.Id)).ToList();
            }
            if (managerId.HasValue)
            {
                project.Manager = _context.Workers.Include(x=>x.Projects).First(x => x.Id == managerId.Value);
            }
            return project;
        }

        public Project AddJobsToProject(Project project, IEnumerable<int>? selectedJobs)
        {
            if (selectedJobs != null)
            {
                project.Jobs = _context.Jobs.Where(x => selectedJobs.Contains(x.Id)).ToList();
            }
            return project;
        }

        public void Delete(int id)
        {
            var project = _context.Projects
                .Include(x=>x.Workers)
                .Include(x => x.Manager)
                .Include(x=>x.Jobs)
                .First(x => x.Id == id);
            Remove(project);
        }

        public Project GetById(int id)
        {
            return _context.Projects.Include(x => x.Manager)
                                    .Include(x => x.Workers)
                                    .Include(x=>x.Jobs)
                                    .First(x => x.Id == id);
        }
    }
}
