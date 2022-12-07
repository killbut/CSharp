using System.Linq.Expressions;
using Application.Models.Workers;
using Core.Entities;

namespace Application.Services.Interfaces
{
    public interface IWorkerService
    {
        IEnumerable<WorkerViewModel> GetWorkers(Expression<Func<Worker, bool>> predicate = null, 
                                                Func<IQueryable<Worker>, IOrderedQueryable<Worker>> orderBy = null, 
                                                string includes = null);
        IEnumerable<WorkerViewModel> GetByName(string name);
        WorkerEditViewModel GetWhenEdit(int id);
        WorkerViewModel GetById(int id);
        int Create(WorkerCreateViewModel workerCreateModel);
        void Delete(int id);
        int Update(WorkerEditViewModel workerModel);
    }
}
