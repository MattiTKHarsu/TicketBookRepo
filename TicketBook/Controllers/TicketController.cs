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

        //
        // All Tickets are returned in response message
        //
        [HttpGet]
        public async Task<ActionResult<List<Ticket>>> Get()
        {
             
            var tickets = from t in _context.Tickets
                            select t;
            List<Ticket> response = new List<Ticket>();
            Console.WriteLine("GET");

            try {
                // Only the open Tickets will be inserted into response,
                // and then the response will ordered by category and then
                // by Timestamp from newer Tickets to older Tickets
                //
                tickets = tickets.OrderBy(t => t.Category)
                                 .ThenByDescending(t => t.TimeStamp)
                                 .Where(t => t.Status == StatusClass.Open);

                response = await tickets.ToListAsync();
            }
            catch
            {
                // This message should be written to logfile, but the logservice is missing !!
                Console.WriteLine("ERROR: Database connection failed. ");
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "Cannot Read data from Repository");
            }
            return Ok( response );
        }

        //
        // Requested Ticket is returned in response message
        //
        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> Get(int id)
        {

            try
            {
                var ticket = await _context.Tickets.FindAsync(id);
                if (ticket == null) return BadRequest("Ticket not found");

                return Ok(ticket);
            }
            catch
            {
                // This message should be written to logfile, but the logservice is missing !!
                Console.WriteLine("ERROR: Database connection failed. ");
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "Cannot Read data from Repository");
            }
        }

        //
        // Requested Ticket is returned in response message
        //
        [HttpGet("Device/{id}")]
        public async Task<ActionResult<Ticket>> GetWithDevice(int id)
        {

            Console.WriteLine("Request OK");
            return Ok();
            //try
            //{
            //    var ticket = await _context.Tickets.FindAsync(id);
            //    if (ticket == null) return BadRequest("Ticket not found");

            //    return Ok(ticket);
            //}
            //catch
            //{
                // This message should be written to logfile, but the logservice is missing !!
            //    Console.WriteLine("ERROR: Database connection failed. ");
            //    return StatusCode(StatusCodes.Status503ServiceUnavailable, "Cannot Read data from Repository");
            //}
        }


        //
        // Ticket is stored into database and updated tickets were returned.
        //
        [HttpPost]
        public async Task<ActionResult<List<Ticket>>> AddTicket(Ticket ticket)
        {
          
            ticket.TimeStamp = DateTime.Now;
            _context.Tickets.Add(ticket);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                // This message should be written to logfile, but the logservice is missing !!
                Console.WriteLine("ERROR: Database connection failed. ");
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "Cannot Add data to Repository");
            }
            return Ok(await _context.Tickets.ToListAsync());
        }

        //
        // Ticket is updated in the database and updated tickets were returned.
        //

        [HttpPut]
        public async Task<ActionResult<List<Ticket>>> UpdateTicket(Ticket request)
        {

            try
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
            catch
            {
                // This message should be written to logfile, but the logservice is missing !!
                Console.WriteLine("ERROR: Database connection failed. ");
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "Cannot Update data to Repository");
            }
        }

        //
        // Ticket is deleted in the database and updated tickets were returned.
        //
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Ticket>>> Delete(int id)
        {
            try
            {
                var dbTicket = await _context.Tickets.FindAsync(id);
                if (dbTicket == null) return BadRequest("Ticket not found");

                _context.Tickets.Remove(dbTicket);
                await _context.SaveChangesAsync();
                return Ok(await _context.Tickets.ToListAsync());
            }
            catch
            {
                // This message should be written to logfile, but the logservice is missing !!
                Console.WriteLine("ERROR: Database connection failed. ");
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "Cannot Remove data from Repository");
            }
        }
    }
}

