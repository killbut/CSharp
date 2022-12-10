using Application.Models.Jobs;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers;

public class JobController : Controller
{
    private readonly IJobService _jobService;
    
    public JobController(IJobService jobService)
    {
        _jobService = jobService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(_jobService.GetJobs(includes:"Author Performer"));
    }

    [HttpGet]
    public IActionResult View(int id)
    {
        return View(_jobService.GetById(id));
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(_jobService.GetWhenCreate());
    }

    [HttpPost]
    public IActionResult Create(JobCreateViewModel job)
    {
        if (ModelState.IsValid)
        {
            int id = _jobService.Create(job);
            return RedirectToAction("View", new { id });
        }
        return BadRequest();
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        return View(_jobService.GetWhenEdit(id));
    }

    [HttpPost]
    public IActionResult Edit(JobEditViewModel job)
    {
        if (ModelState.IsValid)
        {
            var id=_jobService.Update(job);
            return RedirectToAction("View", "Job", new { id });
        }
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        _jobService.Delete(id);
        return RedirectToAction("Index","Job");
    }
}