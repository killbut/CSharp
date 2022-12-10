using Application.Models.Workers;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers
{
    public class WorkerController : Controller
    {
        private readonly IWorkerService _workerService;
        
        public WorkerController(IWorkerService workerService)
        {
            _workerService = workerService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var workers = _workerService.GetWorkers(includes:"Projects Jobs");
            return View(workers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Create(WorkerCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var id=_workerService.Create(model);
                return RedirectToAction("View",new{id});
            }
            return View();
        }
        
        [HttpGet]
        public IActionResult View(int id)
        {
            var worker = _workerService.GetById(id);
            return View(worker);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var worker = _workerService.GetWhenEdit(id);
            return View(worker);
        }
        
        [HttpPost]
        public IActionResult Edit(WorkerEditViewModel worker)
        {
            if (ModelState.IsValid)
            {
                _workerService.Update(worker);
            }
            return RedirectToAction("View", new { worker.Id });
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _workerService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
