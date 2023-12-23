using Microsoft.AspNetCore.Mvc;
using Todos.Data.Contexts;
using Todos.Data.Entities;

namespace Todos.MiniAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoTasksController : ControllerBase
    {
        private readonly TodosDbContext _context;

        public TodoTasksController(TodosDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CreateTodoTask(TodoTasks todoTask)
        {
            if (ModelState.IsValid)
            {
                _context.Tasks.Add(todoTask);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetTodoTaskById), new { id = todoTask.Id }, todoTask);
            }
            return BadRequest(ModelState);
        }

        [HttpGet("{id}")]
        public IActionResult GetTodoTaskById(int id)
        {
            var todoTask = _context.Tasks.Find(id);
            if (todoTask == null)
            {
                return NotFound();
            }
            return Ok(todoTask);
        }
    }
}

