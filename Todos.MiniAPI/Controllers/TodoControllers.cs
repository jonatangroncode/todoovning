using Microsoft.AspNetCore.Mvc;
using Todos.Data.Contexts;
using Todos.Data.Entities;

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

            return CreatedAtAction(nameof(GetTodoById), new
            {
                id = newTodo.UserId
            }, newTodo);
        }

        [HttpGet("todos/{id}")]
        public IActionResult GetTodoById(int id)
        {
            var todo = _context.Todos.Find(id);
            if (todo == null)
            {
                return NotFound();
            }
            return Ok(todo);
        }
    }
}