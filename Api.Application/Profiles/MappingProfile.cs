using Api.Application.DTOs;
using Api.Domain.Entities;
using AutoMapper;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Api.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreaterTaskToDo, TaskToDo>();
        }
    }
}
