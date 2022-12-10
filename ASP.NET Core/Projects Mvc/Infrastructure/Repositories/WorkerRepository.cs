using Core.Entities;
using Core.Repositories;
using Infrastructure.Context;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class WorkerRepository : BaseRepository<Worker>, IWorkerRepository
    {
        public WorkerRepository(DataDbContext context) : base(context) { }

        public void Delete(int id)
        {
            var worker = _context.Workers
                .Include(x => x.Projects)
                .Include(x=>x.Jobs)
                .First(x => x.Id == id);
            Remove(worker);
        }

        public Worker GetById(int id)
        {
            return _context.Workers.Include(x => x.Projects)
                .Include(x=>x.Jobs)
                .First(x=>x.Id==id);
        }
    }
}
