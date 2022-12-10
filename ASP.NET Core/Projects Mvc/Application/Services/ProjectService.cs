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
            var mappedWorkers = ObjectMapper.Mapper.Map<IEnumerable<WorkerViewModel>>(_workerRepository.GetAll());
            var mappedJobs = ObjectMapper.Mapper.Map<IEnumerable<JobViewModel>>(_jobRepository.GetAll());
            mappedProject.AvailableWorkers = mappedWorkers;
            mappedProject.AvailableJobs = mappedJobs;
            return mappedProject;
        }
        public ProjectCreateViewModel GetWhenCreate()
        {
            var project = new ProjectCreateViewModel();
            var mappedProject = ObjectMapper.Mapper.Map<ProjectCreateViewModel>(project);
            var workers = _workerRepository.GetAll();
            var jobs = _jobRepository.GetAll();
            var mappedWorkers = ObjectMapper.Mapper.Map<IEnumerable<WorkerViewModel>>(workers);
            var mappedJobs = ObjectMapper.Mapper.Map<IEnumerable<JobViewModel>>(jobs);
            mappedProject.AvailableWorkers = mappedWorkers;
            mappedProject.AvailableJobs = mappedJobs;
            return mappedProject;
        }
        public ProjectViewModel GetById(int id)
        {
            var project = _projectRepository.GetById(id);
            return ObjectMapper.Mapper.Map<ProjectViewModel>(project);
        }

        public int Create(ProjectCreateViewModel project)
        {
            var mappedProject = ObjectMapper.Mapper.Map<Project>(project);
            mappedProject=_projectRepository.AddWorkersToProject(mappedProject, project.SelectedWorkersId, project.SelectedManagerId);
            mappedProject = _projectRepository.AddJobsToProject(mappedProject, project.SelectedJobs);
            _projectRepository.Add(mappedProject);
            return mappedProject.Id;
        }

        public void Delete(int id)
        {
            _projectRepository.Delete(id);
        }

        public int Update(ProjectEditViewModel project)
        {
            var mappedProject = ObjectMapper.Mapper.Map<Project>(project);
            mappedProject = _projectRepository.AddJobsToProject(mappedProject, project.SelectedJobs);
            mappedProject = _projectRepository.AddWorkersToProject(mappedProject, project.SelectedWorkersId, project.SelectedManagerId);
            _projectRepository.Update(mappedProject);
            return mappedProject.Id;
        }
    }
}
