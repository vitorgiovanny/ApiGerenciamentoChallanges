using Api.Constante.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Application.DTOs
{
    public class CreaterTaskToDo
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public TaskToDoStatus Status { get; set; }
        public TaskPriority TaskPriority { get; set; }

        public CreaterTaskToDo() { }

        public CreaterTaskToDo(string title, string description, DateTime dueDate, TaskToDoStatus status, TaskPriority priority)
        {
            Title = title;
            Description = description;
            DueDate = dueDate;
            Status = status;
            TaskPriority = priority;
        }

        public bool ValidateEmpty()
            => !string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Description);

        public bool IsNotValidateLength()
            => Title.Length < 3 || Description.Length < 3;
    }
}
