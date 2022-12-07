using Application.Models.Projects;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService service)
        {
             _projectService= service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_projectService.GetProjects(Includes:"Manager Workers"));
        }
        
        [HttpPost]
        public IActionResult Index(DateTime? start,ushort? priority)
        {
            if (start.HasValue)
                return View(_projectService.GetProjects(predicate: x=>x.StartTime.ToShortDateString()==start.Value.ToShortDateString()));
            if (priority.HasValue)
                return View(_projectService.GetProjects(predicate:x=>x.Priority==priority.Value));
            return Index();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(_projectService.GetWhenCreate());
        }

        [HttpPost]
        public IActionResult Create(ProjectCreateViewModel project)
        {
            if (ModelState.IsValid)
            {
                int id=_projectService.Create(project);
                return RedirectToAction("View",new{id});
            }
            return View(_projectService.GetWhenCreate());
        }

        [HttpGet]
        public IActionResult View(int id)
        {
            var project = _projectService.GetById(id);
            return View(project);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var project = _projectService.GetWhenEdit(id);
            return View(project);
        }

        [HttpPost]
        public IActionResult Edit(ProjectEditViewModel project)
        {
            if (ModelState.IsValid)
            {
                var id=_projectService.Update(project);
                return RedirectToAction("View",new{id});
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _projectService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
