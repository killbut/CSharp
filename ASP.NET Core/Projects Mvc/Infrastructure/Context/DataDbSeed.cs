using Core.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Context
{
    public static class DataDbSeed
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using (var context = serviceProvider.GetService<DataDbContext>())
            {
                try
                {
                    if (!context.Workers.Any())
                    {
                        AddWorkers(context);
                    }
                    if (!context.Projects.Any())
                    {
                        AddProjects(context);
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
        private static void AddWorkers(DataDbContext context)
        {
            var worker = new[]
            {
                new Worker{ FirstName="Иван",LastName="Иванов",Patronymic="Иванович",Email="Ivan@mail.ru"},
                new Worker{ FirstName="Дмитрий",LastName="Кислин",Patronymic="Алексеевич",Email="Dima@mail.ru"},
                new Worker{ FirstName="Мага",LastName="Магомед",Patronymic="Магомедович",Email="Magodan@mail.ru"}
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
    }
}
