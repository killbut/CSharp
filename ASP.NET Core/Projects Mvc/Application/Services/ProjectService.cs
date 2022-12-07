using System.Linq.Expressions;
using Application.AutoMapper;
using Application.Models.Projects;
using Application.Models.Workers;
using Application.Services.Interfaces;
using Core.Entities;
using Core.Repositories;

namespace Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IWorkerRepository _workerRepository;

        public ProjectService(IProjectRepository projectRepository,IWorkerRepository workerRepository)
        {
            _projectRepository = projectRepository;
            _workerRepository = workerRepository;
        }
        
        public IEnumerable<ProjectViewModel> GetProjects(Expression<Func<Project, bool>> predicate = null, 
                                                Func<IQueryable<Project>, IOrderedQueryable<Project>> orderBy = null, 
                                                string includes = null)
        {

            var projects = _projectRepository.GetAll(predicate, orderBy, includes);
            return ObjectMapper.Mapper.Map<IEnumerable<ProjectViewModel>>(projects);
        }

        public IEnumerable<ProjectViewModel> GetByName(string name)
        {
            var projects = _projectRepository.GetAll(predicate: x=>x.ProjectName.Contains(name) || x.CompanyCustomer.Contains(name) || x.CompanyExecutor.Contains(name));
            return ObjectMapper.Mapper.Map<IEnumerable<ProjectViewModel>>(projects);
        }

        public ProjectEditViewModel GetWhenEdit(int id)
        {
            var project = _projectRepository.Get(id);
            var mappedProject = ObjectMapper.Mapper.Map<ProjectEditViewModel>(project);
            var workers = _workerRepository.GetAll();
            var mappedWorkers = ObjectMapper.Mapper.Map<IEnumerable<WorkerViewModel>>(workers);
            mappedProject.AvailableWorkers = mappedWorkers;
            return mappedProject;
        }
        public ProjectCreateViewModel GetWhenCreate()
        {
            var project = new ProjectCreateViewModel();
            var mappedProject = ObjectMapper.Mapper.Map<ProjectCreateViewModel>(project);
            var workers = _workerRepository.GetAll();
            var mappedWorkers = ObjectMapper.Mapper.Map<IEnumerable<WorkerViewModel>>(workers);
            mappedProject.AvailableWorkers = mappedWorkers;
            return mappedProject;
        }
        public ProjectViewModel GetById(int id)
        {
            var project = _projectRepository.GetById(id);
            return ObjectMapper.Mapper.Map<ProjectViewModel>(project);
        }

        public int Create(ProjectCreateViewModel project)
        {
            var mapped = ObjectMapper.Mapper.Map<Project>(project);
            var newProject=_projectRepository.AddWorkersToProject(mapped, project.SelectedWorkersId, project.SelectedManagerId);
            _projectRepository.Add(newProject);
            return mapped.Id;
        }

        public void Delete(int id)
        {
            _projectRepository.Delete(id);
        }

        public int Update(ProjectEditViewModel project)
        {
            throw new NotImplementedException();
        }
    }
}
