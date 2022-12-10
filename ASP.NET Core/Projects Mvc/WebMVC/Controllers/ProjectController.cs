using Application.Models.Projects;
using Application.Services.Interfaces;
using Core.Entities;
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
        public IActionResult Index(string typeSorting = null, string typeFilter = null)
        {
            var sorting = Sorting(typeSorting);
            return View(_projectService.GetProjects(orderBy:sorting,Includes:"Manager Workers Jobs"));
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

        private Func<IQueryable<Project>, IOrderedQueryable<Project>>Sorting(string sort) //todo bad code
        {
            ViewData["NameSort"]=sort=="Name" ? "NameDesc":"Name";
            ViewData["CustomerSort"] = sort=="Customer" ? "CustomerDesc" : "Customer";
            ViewData["ExecutorSort"]= sort=="Executor" ? "ExecutorDesc" : "Executor";
            ViewData["ManagerSort"] = sort=="Manager" ? "ManagerDesc" : "Manager";
            ViewData["WorkersSort"] = sort=="Worker" ? "WorkerDesc" : "Worker";
            ViewData["JobsSort"]=sort=="Job"? "JobsDesc" : "Job";
            ViewData["DateStartSort"] = sort=="DateStart" ? "DateStartDesc" : "DateStart";
            ViewData["DateEndSort"]=sort=="DateEnd" ? "DateEndDesc" : "DateEnd";
            ViewData["PrioritySort"]=sort=="Priority" ? "PriorityDesc" : "Priority";
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "NameDesc":
                        return new Func<IQueryable<Project>, IOrderedQueryable<Project>>(x => x.OrderByDescending(x => x.ProjectName));
                    case "Name":
                        return new Func<IQueryable<Project>, IOrderedQueryable<Project>>(x => x.OrderBy(x => x.ProjectName));
                    case "CustomerDesc":
                        return new Func<IQueryable<Project>, IOrderedQueryable<Project>>(x => x.OrderByDescending(x => x.CompanyCustomer));
                    case "Customer":
                        return new Func<IQueryable<Project>, IOrderedQueryable<Project>>(x =>
                            x.OrderBy(x => x.CompanyCustomer));
                    case "ExecutorDesc":
                        return new Func<IQueryable<Project>, IOrderedQueryable<Project>>(x =>
                            x.OrderByDescending(x => x.CompanyExecutor));
                    case "Executor":
                        return new Func<IQueryable<Project>, IOrderedQueryable<Project>>(x =>
                            x.OrderBy(x => x.CompanyExecutor));
                    case "ManagerDesc":
                        return new Func<IQueryable<Project>, IOrderedQueryable<Project>>(x =>
                            x.OrderByDescending(x => x.Manager));
                    case "Manager":
                        return new Func<IQueryable<Project>, IOrderedQueryable<Project>>(x =>
                            x.OrderBy(x => x.Manager));
                    case "WorkerDesc":
                        return new Func<IQueryable<Project>, IOrderedQueryable<Project>>(x =>
                            x.OrderByDescending(x => x.Workers.Count));
                    case "Worker":
                        return new Func<IQueryable<Project>, IOrderedQueryable<Project>>(x =>
                            x.OrderBy(x => x.Workers.Count));
                    case "JobsDesc":
                        return new Func<IQueryable<Project>, IOrderedQueryable<Project>>(x =>
                            x.OrderByDescending(x => x.Jobs.Count));
                    case "Jobs":
                        return new Func<IQueryable<Project>, IOrderedQueryable<Project>>(x => x.OrderBy(x => x.Jobs));
                    case "DateStartDesc":
                        return new Func<IQueryable<Project>, IOrderedQueryable<Project>>(x =>
                            x.OrderByDescending(x => x.StartTime));
                    case "DateStart":
                        return new Func<IQueryable<Project>, IOrderedQueryable<Project>>(x =>
                            x.OrderBy(x => x.StartTime));
                    case "DateEndDesc":
                        return new Func<IQueryable<Project>, IOrderedQueryable<Project>>(x =>
                            x.OrderByDescending(x => x.EndTime));
                    case "DateEnd":
                        return new Func<IQueryable<Project>, IOrderedQueryable<Project>>(x =>
                            x.OrderBy(x => x.EndTime));
                    case "PriorityDesc":
                        return new Func<IQueryable<Project>, IOrderedQueryable<Project>>(x =>
                            x.OrderByDescending(x => x.Priority));
                    case "Priority":
                        return new Func<IQueryable<Project>, IOrderedQueryable<Project>>(x =>
                            x.OrderBy(x => x.Priority));
                }
            }
            return null;
        }
    }
}
