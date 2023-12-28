namespace Todos.MiniAPI.Controllers
{
    /// <summary>
    /// Controller for managing TodoTasks in the API.
    /// </summary>
    [ApiController]
    [Route("apiTodo/[controller]")]
    public class TodoTasksController : ControllerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TodoTasksController"/> class.
        /// </summary>
        private readonly TodosDbContext _context;

        public TodoTasksController(TodosDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new TodoTask, its like the items thats in a list and todo is the name of the list
        /// </summary>
        /// <param name="description">The description the name of the item ex milk in a shopinglist TodoTask, .</param>
        /// <param name="category">The category of the TodoTask, if its food items it would be ex food.</param>
        /// <param name="todoId">The ID of the associated Todo.</param>
        /// <returns>No content if successful, BadRequest if invalid data.</returns>
        [HttpPost("todotasks")]
        public IActionResult CreateTodoTask(string description, string category, int todoId)
        {
            // If the description is empty or contains only whitespace, return a BadRequest.
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

        /// <summary>
        /// Gets all TodoTasks.
        /// </summary>
        /// <returns>Ok with a list of TodoTasks if available, NotFound if none found.</returns>
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

        /// <summary>
        /// Updates an existing TodoTask.
        /// </summary>
        /// <param name="id">The ID of the TodoTask to update.</param>
        /// <param name="description">The new description of the TodoTask. the new name of the item</param>
        /// <param name="category">The new category of the TodoTask.</param>
        /// <returns>No content if successful, NotFound if TodoTask not found.</returns>
        [HttpPut("updateTodoTask/{id}")]
        public IActionResult UpdateTodoTask(int id, string description, string category)
        {
            // Find the existing TodoTask in the database based on the provided ID.
            var existingTodoTask = _context.Tasks.Find(id);

            // If the TodoTask does not exist, return a NotFound response.
            if (existingTodoTask == null)
            {
                return NotFound();
            }

            // Update properties of the existingTodoTask with values from the query parameters.
            // This includes updating the description and category.
            existingTodoTask.Description = description;
            existingTodoTask.Category = category;

            _context.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Deletes a TodoTask.
        /// </summary>
        /// <param name="id">The ID of the TodoTask to delete.</param>
        /// <returns>No content if successful, NotFound if TodoTask not found.</returns>
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
