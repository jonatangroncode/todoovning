using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Todos.Data.Contexts; // Se till att inkludera rätt namespace

namespace Todos.Data.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublisherController : ControllerBase
    {
        private readonly DataContext _context;

        public PublisherController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Publisher>> GetPublishers()
        {
            var publishers = _context.Publishers.ToList();
            return Ok(publishers);
        }

        [HttpPost]
        public ActionResult<Publisher> CreatePublisher([FromBody] Publisher publisher)
        {
            _context.Publishers.Add(publisher);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetPublishers), new { id = publisher.Id }, publisher);
        }

        // Andra metoder för att uppdatera och radera förläggare kan implementeras på ett liknande sätt

        // Exempel på en metod för att uppdatera en förläggare (PUT)
        [HttpPut("{id}")]
        public IActionResult UpdatePublisher(int id, [FromBody] Publisher updatedPublisher)
        {
            var existingPublisher = _context.Publishers.FirstOrDefault(p => p.Id == id);

            if (existingPublisher == null)
            {
                return NotFound();
            }

            // Uppdatera egenskaper för den befintliga förläggaren med värdena från updatedPublisher

            _context.SaveChanges();
            return NoContent();
        }

        // Exempel på en metod för att ta bort en förläggare (DELETE)
        [HttpDelete("{id}")]
        public IActionResult DeletePublisher(int id)
        {
            var publisherToDelete = _context.Publishers.FirstOrDefault(p => p.Id == id);

            if (publisherToDelete == null)
            {
                return NotFound();
            }

            _context.Publishers.Remove(publisherToDelete);
            _context.SaveChanges();

            return NoContent();
        }
    }
}

