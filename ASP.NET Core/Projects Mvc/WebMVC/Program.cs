using Application.Services;
using Application.Services.Interfaces;
using Core.Repositories;
using Core.Repositories.Base;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DataDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));
builder.Services.AddScoped(typeof(IRepository<>),typeof(BaseRepositories<>)); 
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IWorkerRepository, WorkerRepository>();

builder.Services.AddScoped<IWorkerService, WorkerService>();
builder.Services.AddScoped<IProjectService, ProjectService>();

builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
   app.UseDeveloperExceptionPage();
}
//todo projectworker entity
app.UseHttpsRedirection();
//app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute("default","{controller=Home}/{action=Index}/{id?}");
app.Run();
