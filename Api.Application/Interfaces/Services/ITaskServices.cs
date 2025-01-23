using Api.Application.DTOs;
using Api.Domain.Entities;

namespace Api.Application.Interfaces.Services
{
    public interface ITaskServices
    {
        Task Creater(CreaterTaskToDo dto);
        Task<List<TaskToDo>> Getall();
        Task<TaskToDo> GetById(int id);
        Task<TaskToDo> Update(TaskToDo dto);
    }
}