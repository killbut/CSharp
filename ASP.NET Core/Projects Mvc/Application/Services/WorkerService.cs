using System.Linq.Expressions;
using Application.AutoMapper;
using Application.Models.Workers;
using Application.Services.Interfaces;
using Core.Entities;
using Core.Repositories;

namespace Application.Services
{
    public class WorkerService : IWorkerService
    {
        private readonly IWorkerRepository _workerRepository;

        public WorkerService(IWorkerRepository repository)
        {
            _workerRepository = repository;
        }
        public IEnumerable<WorkerViewModel> GetWorkers(Expression<Func<Worker, bool>> predicate = null, Func<IQueryable<Worker>, IOrderedQueryable<Worker>> orderBy = null, string includes = null)
        {
            var workers= _workerRepository.GetAll(predicate, orderBy, includes);
            return ObjectMapper.Mapper.Map<IEnumerable<WorkerViewModel>>(workers);
        }

        public IEnumerable<WorkerViewModel> GetByName(string name)
        {
            var workers = _workerRepository.GetAll(predicate: x =>
                x.FirstName.Contains(name) || x.LastName.Contains(name) || x.Patronymic.Contains(name));
            return ObjectMapper.Mapper.Map<IEnumerable<WorkerViewModel>>(workers);
        }

        public WorkerEditViewModel GetWhenEdit(int id)
        {
            var worker = _workerRepository.Get(id);
            var mapped = ObjectMapper.Mapper.Map<WorkerEditViewModel>(worker);
            return mapped;
        }

        public WorkerViewModel GetById(int id)
        {
            var worker = _workerRepository.GetById(id);
            return ObjectMapper.Mapper.Map<WorkerViewModel>(worker);
        }

        public int Create(WorkerCreateViewModel workerCreateModel)
        {
            var worker = ObjectMapper.Mapper.Map<Worker>(workerCreateModel);
            _workerRepository.Add(worker);
            return worker.Id;
        }

        public void Delete(int id)
        {
            _workerRepository.Delete(id);
        }

        public int Update(WorkerEditViewModel workerModel)
        {
            var worker = _workerRepository.GetById(workerModel.Id);
            ObjectMapper.Mapper.Map<WorkerEditViewModel,Worker>(workerModel,worker);
            _workerRepository.Update(worker);
            return worker.Id;
        }
    }
}
