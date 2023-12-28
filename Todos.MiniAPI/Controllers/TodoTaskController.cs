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

        [HttpPut("updateTodoTask/{id}")]
        public IActionResult UpdateTodoTask(int id, string description, string category)
        {
            var existingTodoTask = _context.Tasks.Find(id);

            if (existingTodoTask == null)
            {
                return NotFound();
            }

            // Update properties of the existingTodoTask with values from the query parameters
            existingTodoTask.Description = description;
            existingTodoTask.Category = category;

            _context.SaveChanges();

            return NoContent();
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



    }


}
