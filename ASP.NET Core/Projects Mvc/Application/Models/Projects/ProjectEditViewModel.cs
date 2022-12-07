﻿using Application.Models.Workers;

namespace Application.Models.Projects;

public class ProjectEditViewModel
{
    public int Id { get; set; }
    public string ProjectName { get; set; }
    public string CompanyCustomer { get; set; }
    public string CompanyExecutor { get; set; }
    public IEnumerable<WorkerViewModel> Workers { get; set; }
    public WorkerViewModel? Manager;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public ushort Priority { get; set; }
    
    public IEnumerable<WorkerViewModel>? AvailableWorkers { get; set; }
    public int[]? SelectedWorkersId { get; set; }
    public int? SelectedManagerId { get; set; }
}