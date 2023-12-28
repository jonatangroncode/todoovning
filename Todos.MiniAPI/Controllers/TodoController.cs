
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
        // post with user 
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
            var todos = _context.Todos.ToList();

            if (todos == null || todos.Count == 0)
            {
                return NotFound();
            }

            return Ok(todos); //Returns all Todos
        }



        [HttpPut("todos/{id}")]
        public IActionResult UpdateTodo(int id, string title, int userId)
        {
            var existingTodo = _context.Todos.FirstOrDefault(t => t.Id == id);// Get the first object with a matching 'Id'

            if (existingTodo == null)
            {
                return NotFound(); // Returns 404
            }

            if (string.IsNullOrWhiteSpace(title))
            {
                ModelState.AddModelError("Title", "The Title field is required.");
                return BadRequest(ModelState);
            }

            existingTodo.Title = title;
            existingTodo.UserId = userId;

            _context.Todos.Update(existingTodo);
            _context.SaveChanges();

            return Ok(existingTodo); // Returns updated todo
        }
        [HttpDelete("todos/{id}")]
        public IActionResult DeleteTodo(int id)
        {
            var todoToDelete = _context.Todos.Find(id);

            if (todoToDelete == null)
            {
                return NotFound();
            }

            _context.Todos.Remove(todoToDelete);
            _context.SaveChanges();

            return NoContent();


        }
        }
    }


