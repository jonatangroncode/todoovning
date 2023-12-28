using Microsoft.AspNetCore.Mvc;
using Todos.Data.Contexts;
using Todos.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Todos.MiniAPI.Controllers
{
    [ApiController]
    [Route("apiTodo/[controller]")]
    public class TodoTasksController : ControllerBase
    {
        private readonly TodosDbContext _context;

        public TodoTasksController(TodosDbContext context)
        {
            _context = context;
        }

        [HttpPost("todotasks")]
        public IActionResult CreateTodoTask(string description, string category, int todoId)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                ModelState.AddModelError("Description", "The Description field is required.");
                return BadRequest(ModelState);
            }

            var newTodoTask = new TodoTasks
            {
                Description = description,
                Category = category,
                TodoId = todoId
            };

            _context.Tasks.Add(newTodoTask);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpGet("getTodoTasks")]
        public IActionResult GetAllTodoTasks()
        {
            var todoTasks = _context.Tasks.ToList();

            if (todoTasks == null || todoTasks.Count == 0)
            {
                return NotFound();
            }

            return Ok(todoTasks);
        }

        [HttpDelete("deleteTodoTask/{id}")]
        public IActionResult DeleteTodoTask(int id)
        {
            var todoTask = _context.Tasks.Find(id);

            if (todoTask == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(todoTask);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPut("updateTodoTask/{id}")]
        public IActionResult UpdateTodoTask(int id, [FromBody] TodoTasks updatedTodoTask)
        {
            if (id != updatedTodoTask.Id)
            {
                return BadRequest();
            }

            var existingTodoTask = _context.Tasks.Find(id);

            if (existingTodoTask == null)
            {
                return NotFound();
            }

            // Update properties of the existingTodoTask with values from updatedTodoTask
            existingTodoTask.Description = updatedTodoTask.Description;
            existingTodoTask.Category = updatedTodoTask.Category;

            _context.SaveChanges();

            return NoContent();
        }
    }


}
