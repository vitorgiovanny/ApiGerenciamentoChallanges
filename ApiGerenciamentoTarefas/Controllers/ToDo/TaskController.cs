using Api.Application.DTOs;
using Api.Application.Interfaces.Services;
using Api.Constante.Entities;
using Api.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiGerenciamentoTarefas.Controllers.ToDo
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskServices _services;

        public TaskController(ITaskServices services) 
        {
            _services = services;
        }

        [Authorize()]
        [HttpPost("Creater")]
        public async Task<IActionResult> CreaterTask([FromBody] CreaterTaskToDo dto)
        {
            try
            {
                await _services.Creater(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize()]
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateTaskToDo([FromBody] TaskToDo task)
            => Ok(new {Message = "Updated a Task", response = await _services.Update(task)});


        [Authorize()]
        [HttpGet("GetaAllTaskToDo")]
        public async Task<IActionResult> GetAllTask()
        {
            return Ok(new {Message = "List with Task To Do", response = await _services.Getall()});
        }

        [Authorize()]
        [HttpGet("GetTaskById")]
        public async Task<IActionResult> GetTaskById([FromQuery] int id)
        {
            return Ok(new {Message = "Task by Id", response = await _services.GetById(id)});
        }

        [Authorize()]
        [HttpPut("removerTask")]
        public async Task<IActionResult> RemoveTask([FromBody] TaskToDo dto)
        {
            try
            {
                var response = await _services.Update(dto);
                return Ok(new {Message = "Task Removed", response});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }   

    }
}
