using System.Linq.Expressions;
using Application.Models.Jobs;
using Core.Entities;

namespace Application.Services.Interfaces;

public interface IJobService
{
    IEnumerable<JobViewModel> GetJobs(Expression<Func<Job, bool>> predicate = null, 
                                         Func<IQueryable<Job>, IOrderedQueryable<Job>> orderBy = null, 
                                         string includes = null);
    
    IEnumerable<JobViewModel> GetByName(string name);
    JobEditViewModel GetWhenEdit(int id);
    JobCreateViewModel GetWhenCreate();
    JobViewModel GetById(int id);
    int Create(JobCreateViewModel jobModel);
    void Delete(int id);
    int Update(JobEditViewModel job);
}