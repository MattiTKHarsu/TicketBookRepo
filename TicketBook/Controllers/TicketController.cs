using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicketBook.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TicketBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : Controller
    {
        
        private readonly DataContext _context;

        public TicketController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Ticket>>> Get()
        {
            return Ok(await _context.Tickets.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> Get(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null) return BadRequest("Ticket not found");
            return Ok(ticket);
        }


        [HttpPost]
        public async Task<ActionResult<List<Ticket>>> AddTicket(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
            return Ok(await _context.Tickets.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Ticket>>> UpdateTicket(Ticket request)
        {
            
            var dbTicket = await _context.Tickets.FindAsync(request.Id);
            if (dbTicket == null) return BadRequest("Ticket not found");

            dbTicket.DeviceId    = request.DeviceId;
            dbTicket.Description = request.Description;
            dbTicket.Category    = request.Category;
            dbTicket.Status      = request.Status;

            await _context.SaveChangesAsync();

            return Ok(await _context.Tickets.ToListAsync());
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Ticket>>> Delete(int id)
        {
            var dbTicket = await _context.Tickets.FindAsync(id);
            if (dbTicket == null) return BadRequest("Ticket not found");
        
            _context.Tickets.Remove(dbTicket);
            await _context.SaveChangesAsync();
            return Ok(await _context.Tickets.ToListAsync());
        }
    }
}

