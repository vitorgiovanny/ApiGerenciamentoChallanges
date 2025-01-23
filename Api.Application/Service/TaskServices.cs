using Api.Application.DTOs;
using Api.Application.Interfaces.Services;
using Api.Domain.Entities;
using Api.Domain.Repositories;
using AutoMapper;

namespace Api.Application.Service
{
    public class TaskServices : ITaskServices
    {
        private readonly IRepository<TaskToDo> _repository;
        private readonly IMapper _mapper;

        public TaskServices(IRepository<TaskToDo> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Creater(CreaterTaskToDo dto)
        {
            if (!dto.ValidateEmpty() || dto.IsNotValidateLength()) throw new Exception("Erro de validação dos dados.");

            var mappingTask = _mapper.Map<TaskToDo>(dto);
            await _repository.AddAsync(mappingTask);
            await _repository.SaveChangesAsync();
        }

        public async Task<List<TaskToDo>> Getall()
            => await _repository.GetAllAsync();

        public async Task<TaskToDo> GetById(int id)
            => await _repository.GetByIdAsync(id);

        public async Task<TaskToDo> Update(TaskToDo dto)
        {
            await _repository.Update(dto);
            await _repository.SaveChangesAsync();
            
            return await GetById(dto.Id);
        }

    }
}
