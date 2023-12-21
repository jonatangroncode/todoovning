using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Linq;



namespace Todos.Data.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly DataContext _context;

        public BookController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> Get()
        {
            var books = _context.Books.ToList();
            return Ok(books);
        }

        [HttpPost]
        public ActionResult<Book> Post([FromBody] Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = book.Id }, book);
        }
    }
}
