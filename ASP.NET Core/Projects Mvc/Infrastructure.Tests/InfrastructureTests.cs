using Core.Entities;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests;

public class InfrastructureTests
{
    private DataDbContext _context;
    private ProjectRepository _projectRepository;
    private WorkerRepository _workerRepository;

    public InfrastructureTests()
    {
        Init();
    }
    public void Init()
    {
        var dbOptin = new DbContextOptionsBuilder<DataDbContext>().UseInMemoryDatabase("TestDb").Options;
        _context = new DataDbContext(dbOptin);
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
        _projectRepository = new ProjectRepository(_context);
        _workerRepository = new WorkerRepository(_context);
    }
    [Fact]
    public void Add_Values_ToRepository()
    {
        var worker = new Worker(1, "Firstname_test", "Lastname_test", "Patronymic_test", "123@mail.ru_test", null);
        var project = new Project(1, "Projectname_test", "CompanyCustomer_test", "CompanyExecutor_test", null, null,
            new DateTime(2022, 12, 6), new DateTime(2022, 12, 7), 1);

        _projectRepository.Add(project);
        _workerRepository.Add(worker);
        
        Assert.NotEmpty(_context.Projects);
        Assert.NotEmpty(_context.Workers);
    }
    
    [Fact]
    public void Get_SameValues_InRepository()
    {
        var worker = new Worker(1, "Firstname_test", "Lastname_test", "Patronymic_test", "123@mail.ru_test", null);
        var project = new Project(1, "Projectname_test", "CompanyCustomer_test", "CompanyExecutor_test", null, null,
            new DateTime(2022, 12, 6), new DateTime(2022, 12, 7), 1);

        _projectRepository.Add(project);
        _workerRepository.Add(worker);
        
        Assert.Equal(_context.Projects.First(),project);
        Assert.Equal(_context.Workers.First(),worker);
    }
    
    [Fact]
    public void Check_Includes_InRepository()
    {
        var worker = new Worker(1, "Firstname_test", "Lastname_test", "Patronymic_test", "123@mail.ru_test", null);
        var project = new Project(1, "Projectname_test", "CompanyCustomer_test", "CompanyExecutor_test", null, null,
            new DateTime(2022, 12, 6), new DateTime(2022, 12, 7), 1);
        worker.Projects = new List<Project>() { project };
        
        _workerRepository.Add(worker);
        
        Assert.Equal(_context.Workers.First().Projects.First(),project);
    }
    
    [Fact]
    public void Check_Update_InRepository()
    {
        var worker = new Worker(1, "Firstname_test", "Lastname_test", "Patronymic_test", "123@mail.ru_test", null);
        var oldName = worker.FirstName;

        _workerRepository.Add(worker);
        worker.FirstName = "newTestName";
        _workerRepository.Update(worker);
        
        Assert.NotEqual(_context.Workers.First().FirstName,oldName);
    }
    
    [Fact]
    public void Check_Remove_InRepository()
    {
        var worker = new Worker(1, "Firstname_test", "Lastname_test", "Patronymic_test", "123@mail.ru_test", null);

        _workerRepository.Add(worker);
        _workerRepository.Remove(worker);
        
        Assert.Empty(_context.Workers);
    }
    
    [Fact]
    public void Check_AddNewProjectWithWorkers()
    {
        var worker1 = new Worker(1, "Firstname1_test", "Lastname1_test", "Patronymic1_test", "1@mail.ru_test", null);
        var worker2 = new Worker(2, "Firstname2_test", "Lastname2_test", "Patronymic2_test", "2@mail.ru_test", null);
        var project = new Project(1, "Projectname_test", "CompanyCustomer_test", "CompanyExecutor_test", null, null,
            new DateTime(2022, 12, 6), new DateTime(2022, 12, 7), 1);

        _workerRepository.Add(worker1);
        _workerRepository.Add(worker2);
        var newProject=_projectRepository.AddWorkersToProject(project, new int[]{1,2}, 1);
        _projectRepository.Add(newProject);
        
        Assert.Equal(_context.Projects.First().Manager,worker1);
        Assert.Contains(_context.Projects.First().Workers,new List<ICollection<Worker>>(){new List<Worker>(){worker1,worker2}});
    }
}