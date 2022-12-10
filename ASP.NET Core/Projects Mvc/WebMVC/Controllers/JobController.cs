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
        return View( new JobCreateViewModel());
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
    public IActionResult Edit()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Edit(JobEditViewModel job)
    {
        return View();
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        return Index();
    }
}