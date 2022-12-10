using System.Linq.Expressions;
using Application.Models.Projects;
using Core.Entities;

namespace Application.Services.Interfaces
{
    public interface IProjectService
    {
        IEnumerable<ProjectViewModel> GetProjects(Expression<Func<Project, bool>> predicate = null, 
                                                  Func<IQueryable<Project>, IOrderedQueryable<Project>> orderBy = null, 
                                                  string Includes = null);
        IEnumerable<ProjectViewModel> GetByName(string name);
        ProjectEditViewModel GetWhenEdit(int id);
        ProjectCreateViewModel GetWhenCreate();
        ProjectViewModel GetById(int id);
        int Create(ProjectCreateViewModel projectModel);
        void Delete(int id);
        int Update(ProjectEditViewModel projectModel);
    }
}
