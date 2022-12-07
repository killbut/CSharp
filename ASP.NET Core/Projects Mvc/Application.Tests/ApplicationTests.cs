using Core.Repositories;
using Moq;

namespace Application.Tests;

public class UnitTest1
{
    private Mock<IProjectRepository> _mockProjectRepository;
    private Mock<IWorkerRepository> _mockWorkerRepository;

    public UnitTest1()
    {
        _mockProjectRepository = new Mock<IProjectRepository>();
        _mockWorkerRepository = new Mock<IWorkerRepository>();
    }
    
    [Fact]
    public void Remove_Test()
    {
        
        

    }
}