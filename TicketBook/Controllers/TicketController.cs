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
             
            var tickets = from t in _context.Tickets
                            select t;
            List<Ticket> response = new List<Ticket>();
            Console.WriteLine("ERROR: Database connection failed 1. ");
            try {
                // Only the open Tickets will be inserted into response,
                // and then the response will ordered by category and then
                // by Timestamp from newer Tickets to older Tickets
                //
                Console.WriteLine("ERROR: Database connection failed 2. ");
                tickets = tickets.OrderBy(t => t.Category)
                                 .ThenByDescending(t => t.TimeStamp)
                                 .Where(t => t.Status == StatusClass.Open);

                Console.WriteLine("ERROR: Database connection failed 3. ");
                response = await tickets.ToListAsync();
            }
            catch
            {
                // This message should be written to logfile, but the logfile is missing !!
                Console.WriteLine("ERROR: Database connection failed. ");
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "Cannot read data from Repository");
            }
            return Ok( response);
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
            ticket.TimeStamp = DateTime.Now;
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
            return Ok(await _context.Tickets.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Ticket>>> UpdateTicket(Ticket request)
        {

            var dbTicket = await _context.Tickets.FindAsync(request.Id);
            if (dbTicket == null) return BadRequest("Ticket not found");

            dbTicket.DeviceId = request.DeviceId;
            dbTicket.Description = request.Description;
            dbTicket.Category = request.Category;
            dbTicket.Status = request.Status;

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

