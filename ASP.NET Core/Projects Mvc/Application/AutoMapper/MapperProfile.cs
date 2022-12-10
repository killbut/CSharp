using Application.Models.Jobs;
using Application.Models.Projects;
using Application.Models.Workers;
using AutoMapper;
using Core.Entities;

namespace Application.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Worker, WorkerViewModel>().ReverseMap();
            CreateMap<Worker, WorkerCreateViewModel>().ReverseMap();
            CreateMap<Worker,WorkerEditViewModel>().ReverseMap();
            
            CreateMap<Project,ProjectViewModel>().ForMember(x=>x.Manager,opt=>opt.MapFrom(x=>x.Manager)).ReverseMap();
            CreateMap<Project,ProjectViewModel>().ForMember(x=>x.Workers,opt=>opt.MapFrom(x=>x.Workers)).ReverseMap();
            CreateMap<Project, ProjectCreateViewModel>().ReverseMap();
            CreateMap<Project, ProjectEditViewModel>().ReverseMap();

            CreateMap<Job, JobViewModel>().ReverseMap();
            CreateMap<Job, JobEditViewModel>().ReverseMap();
            CreateMap<Job, JobCreateViewModel>().ReverseMap();
        }
    }
}
