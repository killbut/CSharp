using System.Linq.Expressions;
using Application.AutoMapper;
using Application.Models.Jobs;
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
        private readonly IJobRepository _jobRepository;

        public ProjectService(IProjectRepository projectRepository,IWorkerRepository workerRepository,IJobRepository jobRepository)
        {
            _projectRepository = projectRepository;
            _workerRepository = workerRepository;
            _jobRepository = jobRepository;
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
            var mappedProject = ObjectMapper.Mapper.Map<ProjectEditViewModel>( _projectRepository.GetById(id));
            mappedProject.AvailableWorkers = ObjectMapper.Mapper.Map<IEnumerable<WorkerViewModel>>(_workerRepository.GetAll());
            mappedProject.AvailableJobs = ObjectMapper.Mapper.Map<IEnumerable<JobViewModel>>(_jobRepository.GetAll());
            return mappedProject;
        }
        public ProjectCreateViewModel GetWhenCreate()
        {
            var mappedProject = ObjectMapper.Mapper.Map<ProjectCreateViewModel>( new ProjectCreateViewModel());
            mappedProject.AvailableWorkers  = ObjectMapper.Mapper.Map<IEnumerable<WorkerViewModel>>(_workerRepository.GetAll());
            mappedProject.AvailableJobs = ObjectMapper.Mapper.Map<IEnumerable<JobViewModel>>(_jobRepository.GetAll());
            return mappedProject;
        }
        public ProjectViewModel GetById(int id)
        {
            var project = _projectRepository.GetById(id);
            return ObjectMapper.Mapper.Map<ProjectViewModel>(project);
        }

        public int Create(ProjectCreateViewModel projectModel)
        {
            var mappedProject = ObjectMapper.Mapper.Map<Project>(projectModel);
            _projectRepository.AddWorkersToProject(mappedProject, projectModel.SelectedWorkersId, projectModel.SelectedManagerId);
            _projectRepository.AddJobsToProject(mappedProject, projectModel.SelectedJobs);
            _projectRepository.Add(mappedProject);
            return mappedProject.Id;
        }

        public void Delete(int id)
        {
            _projectRepository.Delete(id);
        }

        public int Update(ProjectEditViewModel projectModel)
        {
            var project = _projectRepository.GetById(projectModel.Id);
            ObjectMapper.Mapper.Map<ProjectEditViewModel, Project>(projectModel, project); 
            _projectRepository.AddJobsToProject(project, projectModel.SelectedJobs);
            _projectRepository.AddWorkersToProject(project, projectModel.SelectedWorkersId, projectModel.SelectedManagerId);
            _projectRepository.Update(project);
            return project.Id;
        }
    }
}
