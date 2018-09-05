using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly TicketContext _context;

        public TicketController(TicketContext context)
        {
            _context = context;

            if (!_context.TicketItems.Any())
            {
                _context.TicketItems.Add(new TicketItem {Concert = "Beyonce"});
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<TicketItem> GetAll()
        {
            return _context.TicketItems.AsNoTracking().ToList();
        }

        [HttpGet("{id}", Name = "GetTicket")]
        public IActionResult GetById(long id)
        {
            var ticket = _context.TicketItems.FirstOrDefault(t => t.Id == id);

            if (ticket == null) return NotFound();

            return new ObjectResult(ticket);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TicketItem ticket)
        {
            if (ticket == null) return BadRequest();

            _context.TicketItems.Add(ticket);
            _context.SaveChanges();

            return CreatedAtRoute("GetTicket", new {id = ticket.Id}, ticket);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] TicketItem ticket)
        {
            if (ticket == null || id != ticket.Id) return BadRequest();

            var existingTicket = _context.TicketItems.FirstOrDefault(t => t.Id == id);

            if (existingTicket == null) return NotFound();

            existingTicket.Concert = ticket.Concert;
            existingTicket.Artist = ticket.Artist;
            existingTicket.Available = ticket.Available;
            _context.TicketItems.Update(existingTicket);
            _context.SaveChanges();

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var ticket = _context.TicketItems.FirstOrDefault(t => t.Id == id);
            if (ticket == null) return NotFound();

            _context.TicketItems.Remove(ticket);
            _context.SaveChanges();

            return new NoContentResult();
        }
    }
}