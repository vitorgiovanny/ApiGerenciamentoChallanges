using Api.Constante.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Domain.Entities
{
    public class TaskToDo
    {
        public int Id { get; set; } // ID único para identificação
        public string Title { get; set; } // Título da tarefa
        public string Description { get; set; } // Descrição da tarefa
        public DateTime DueDate { get; set; } // Data de vencimento
        public TaskToDoStatus Status { get; set; } // Status da tarefa
        public TaskPriority Priority { get; set; } // Prioridade da tarefa
        public DateTime? UpdateDate { get; set; }
    }
}
