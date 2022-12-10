using System.Linq.Expressions;
using Application.AutoMapper;
using Application.Models.Jobs;
using Application.Models.Workers;
using Application.Services.Interfaces;
using Core.Entities;
using Core.Repositories;

namespace Application.Services;

public class JobService : IJobService
{
    private readonly IJobRepository _jobRepository;
    private readonly IWorkerRepository _workerRepository;

    public JobService(IJobRepository jobRepository,IWorkerRepository workerRepository)
    {
        _jobRepository = jobRepository;
        _workerRepository = workerRepository;
    }
    public IEnumerable<JobViewModel> GetJobs(Expression<Func<Job, bool>> predicate = null, Func<IQueryable<Job>, IOrderedQueryable<Job>> orderBy = null, string includes = null)
    {
        var projects = _jobRepository.GetAll(predicate, orderBy, includes);
        return ObjectMapper.Mapper.Map<IEnumerable<JobViewModel>>(projects);
    }

    public IEnumerable<JobViewModel> GetByName(string name)
    {
        var projects = _jobRepository.GetAll(predicate: x=>x.JobName.Contains(name));
        return ObjectMapper.Mapper.Map<IEnumerable<JobViewModel>>(projects);
    }

    public JobEditViewModel GetWhenEdit(int id)
    {
        var job = _jobRepository.Get(id);
        var mappedJob = ObjectMapper.Mapper.Map<JobEditViewModel>(job);
        var workers = _workerRepository.GetAll();
        var mappedWorkers = ObjectMapper.Mapper.Map<IEnumerable<WorkerViewModel>>(workers);
        mappedJob.AvailableWorkers = mappedWorkers;
        return mappedJob;
    }

    public JobCreateViewModel GetWhenCreate()
    {
        var job = new JobCreateViewModel();
        var mapped = ObjectMapper.Mapper.Map<JobCreateViewModel>(job);
        var workers = _workerRepository.GetAll();
        var mappedWorkers = ObjectMapper.Mapper.Map<IEnumerable<WorkerViewModel>>(workers);
        mapped.AvailableWorkers = mappedWorkers;
        return mapped;
    }

    public JobViewModel GetById(int id)
    {
        var job = _jobRepository.Get(id);
        return ObjectMapper.Mapper.Map<JobViewModel>(job);
    }

    public int Create(JobCreateViewModel job)
    {
        var mapped = ObjectMapper.Mapper.Map<Job>(job);
        var newJob=_jobRepository.AddWorkerToJob(mapped, job.SelectedWorkerId, job.AuthorId);
        _jobRepository.Add(newJob);
        return mapped.Id;
    }

    public void Delete(int id)
    {
        _jobRepository.Delete(id);
    }

    public int Update(JobViewModel project)
    {
        throw new NotImplementedException();
    }
}