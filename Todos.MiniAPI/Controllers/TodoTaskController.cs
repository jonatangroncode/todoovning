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
    }
}
