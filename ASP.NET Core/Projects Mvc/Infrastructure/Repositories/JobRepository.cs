using Core.Entities;
using Core.Repositories;
using Infrastructure.Context;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class JobRepository : BaseRepository<Job>, IJobRepository
{
    public JobRepository(DataDbContext context) : base(context)
    {
    }

    public Job AddWorkerToJob(Job job, int? performerId, int authorId)
    {
        if (performerId.HasValue)
        {
            job.Performer = _context.Workers.Include(x => x.Jobs).First(x => x.Id == performerId);
        }

        job.Author = _context.Workers.Include(x => x.Jobs).First(x => x.Id == authorId);
        return job;
    }

    public void Delete(int id)
    {
        var job = _context.Jobs.Include(x => x.Performer).Include(x => x.Author).First(x => x.Id == id);
        Remove(job);
    }
}