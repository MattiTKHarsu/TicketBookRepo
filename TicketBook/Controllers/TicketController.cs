//-----------------------------------------------------------------------------
//   -- T I C K E T   R E P O S I T O R Y   E X A M P L E   P R O G R A M --
//   
//      Company:        Etteplan Oy
//   	
//      Programmer:     Matti Harsu
//
//      Function:       Controller
//
//      Functionality:
//      This program work as Back-End part for recording servive request.
//      Application contains 1) Create record, 2) Read record, 3) Upadate
//      record and 4) Delete record function. In addition all service records
//      and records regarding defined devices can be listed.
//
//      Libraries:
//      - 
//
//-----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicketBook.Models;

namespace TicketBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : Controller
    {

        // Database context is defined for Business Logic  
        private readonly DataContext _context;

        public TicketController(DataContext context)
        {
            _context = context;
        }

        // - - - - -   HTTP API inteface  - - - - -
        //
        // All Tickets are returned in response message
        //
        [HttpGet]
        public async Task<ActionResult<List<Ticket>>> Get()
        {
             
            var tickets = from t in _context.Tickets
                            select t;
            List<Ticket> response = new List<Ticket>();     

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
            var tickets = from t in _context.Tickets
                          select t;
            List<Ticket> response = new List<Ticket>();

            try
            {
                // Lets check first that requested device can be found from db
                var device = await _context.Devices.FindAsync(id);
                if (device == null) return BadRequest("Device not found");

                // Collect Tickets which contains requested DeviceId to response
                tickets = tickets.OrderBy(t => t.Category)
                                 .ThenByDescending(t => t.TimeStamp)
                                 .Where(t => t.DeviceId == id);

                // Database search couldn't get Tickets
                // OBS. 0 Tickets is anyway OK.
                if (tickets == null) return BadRequest("Ticket search didn't succeed");

                response = await tickets.ToListAsync();
                return Ok(response);
            }
            catch
            {
                // This message should be written to logfile, but the logservice is missing !!
                Console.WriteLine("ERROR: Database connection failed. ");
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "Cannot Read data from Repository");
            }
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

