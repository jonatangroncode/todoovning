
namespace Todos.MiniAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly TodosDbContext _context;

        public UserController(TodosDbContext context)
        {
            _context = context;
        }
        // post create a new user
        [HttpPost("users")]
        public IActionResult CreateUser(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                ModelState.AddModelError("Name", "The Name field is required.");
                return BadRequest(ModelState);
            }
            var existingUser = _context.Users.FirstOrDefault(u => u.Name == name);
            if (existingUser != null)
            {
                ModelState.AddModelError("Name", "A user with this name already exists.");
                return Conflict(ModelState); // Returns HTTP  409 (Conflict)
            }

            var newUser = new User { Name = name };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
        }
        // put use user id to get todos and changes name
        [HttpPut("todos/{id}")]
        public IActionResult UpdateTodo(int id, string name)
        {
            User updatedUser = _context.Users.FirstOrDefault(u => u.Id == id);
            if (id != updatedUser.Id)
            {
                return BadRequest();
            }
            updatedUser.Name = name;

            _context.Entry(updatedUser).State = EntityState.Modified;


            try
            {


                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Todos.Any(t => t.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        //get user by id
        [HttpGet("users/{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        // delete user with using id
        [HttpDelete("users/{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            _context.SaveChanges();

            return NoContent();
        }
    }
}

