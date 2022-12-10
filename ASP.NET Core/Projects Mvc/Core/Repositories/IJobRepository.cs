using Core.Entities;
using Core.Repositories.Base;

namespace Core.Repositories;

public interface IJobRepository : IRepository<Job>
{
    Job AddWorkerToJob(Job job, int? performerId, int authorId);
    void Delete(int id);
}