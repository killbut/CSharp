using Application.Models.Workers;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers
{
    public class WorkerController : Controller
    {
        private readonly IWorkerService _service;
        
        public WorkerController(IWorkerService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var workers = _service.GetWorkers(includes:"Projects Jobs");
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
                var id=_service.Create(model);
                return RedirectToAction("View",new{id});
            }
            return View();
        }
        
        [HttpGet]
        public IActionResult View(int id)
        {
            var worker = _service.GetById(id);
            return View(worker);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var worker = _service.GetWhenEdit(id);
            return View(worker);
        }
        
        [HttpPost]
        public IActionResult Edit(WorkerEditViewModel worker)
        {
            if (ModelState.IsValid)
            {
                _service.Update(worker);
            }
            return RedirectToAction("View", new { worker.Id });
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
