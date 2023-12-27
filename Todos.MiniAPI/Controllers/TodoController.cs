using Microsoft.AspNetCore.Mvc;
using Todos.Data.Contexts;
using Todos.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace Todos.MiniAPI.Controllers
{
    [ApiController]
    [Route("apiTodo/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly TodosDbContext _context;

        public TodoController(TodosDbContext context)
        {
            _context = context;
        }

        [HttpPost("todos")]
        public IActionResult CreateTodo(string title, int userId, int id)
        {

            var newTodo = new Todo
            {
                Title = title,
                UserId = userId,
                Id = id


            };

            if (string.IsNullOrWhiteSpace(title))
            {
                ModelState.AddModelError("Name", "The Name field is required.");
                return BadRequest(ModelState);
            }
            _context.Todos.Add(newTodo);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetAllTodos), new { id = newTodo.UserId }, newTodo);
        }

        [HttpGet("getTodos")]
        public IActionResult GetAllTodos()
        {
            var todos = _context.Todos.ToList(); // Hämta alla todos från databasen

            if (todos == null || todos.Count == 0)
            {
                return NotFound(); // Returnera 404 Not Found om det inte finns några todos
            }

            return Ok(todos); // Returnera alla todos om de finns
        }

    }

}
