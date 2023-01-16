using System;
using TicketBook.Controllers;

namespace TicketBook.Models
{
	public class Device
	{
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public int Year { get; set; }
        public string Type { get; set; } = String.Empty;
    }
}

