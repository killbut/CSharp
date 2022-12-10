using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public static class DataDbSeed
    {
        public static void Seed(DataDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.Migrate();
            context.Database.EnsureCreated();
            if (!context.Workers.Any())
            {
                AddWorkers(context);
            }
            if (!context.Projects.Any())
            {
                AddProjects(context);
            }

            if (!context.Jobs.Any())
            {
                AddJobs(context);
            }
        }
        private static void AddWorkers(DataDbContext context)
        {
            var worker = new[]
            {
                new Worker{ FirstName="Кирилл",LastName="Иванов",Patronymic="Матвеевич",Email="Kirill@mail.ru"},
                new Worker{ FirstName="Кира",LastName="Попова",Patronymic="Максимовна",Email="Kira@mail.ru"},
                new Worker{ FirstName="Иван",LastName="Богданов",Patronymic="Алексеевич",Email="Ivan@mail.ru"},
                new Worker{ FirstName="Мирон",LastName="Волков",Patronymic="Никитич",Email="Miron@mail.ru"}
            };
            context.Workers.AddRange(worker);
            context.SaveChanges();
        }
        private static void AddProjects(DataDbContext context)
        {
            var projects = new[]
            {
                new Project{
                    ProjectName="Проект 1",
                    CompanyCustomer="Компания Заказчик 1",
                    CompanyExecutor="Компания исполнитель 1",
                    Manager = context.Workers.First(),
                    Workers =  context.Workers.ToList(),
                    StartTime=DateTime.Today,
                    EndTime=DateTime.Now,
                    Priority=1
                },

                new Project{
                    ProjectName="Проект 2",
                    CompanyCustomer="Компания Заказчик 2",
                    CompanyExecutor="Компания исполнитель 2",
                    Manager = context.Workers.First(),
                    Workers = null,
                    StartTime=DateTime.Today,
                    EndTime=DateTime.Now,
                    Priority=2
                },
            };
            context.Projects.AddRange(projects);
            context.SaveChanges();
        }

        private static void AddJobs(DataDbContext context)
        {
            var jobs = new[]
            {
                new Job()
                {
                    JobName = "Задача_1", Author = context.Workers.First(), Performer = null, Status = JobStatus.ToDo,
                    Description = "Описание задачи 1", Priority = 1
                },
                new Job()
                {
                    JobName = "Задача_2", Author = context.Workers.First(x => x.Id == 2),
                    Performer = context.Workers.First(x => x.Id == 3), Status = JobStatus.ToDo,
                    Description = "Описание задачи 2", Priority = 5
                },
            };
            context.Jobs.AddRange(jobs);
            context.SaveChanges();
        }
    }
    
}
