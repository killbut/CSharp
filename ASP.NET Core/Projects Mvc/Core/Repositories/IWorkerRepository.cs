using Core.Entities;
using Core.Repositories.Base;

namespace Core.Repositories
{
    public interface IWorkerRepository: IRepository<Worker>
    {
        void Delete(int id);
        Worker GetById(int id);
    }
}
