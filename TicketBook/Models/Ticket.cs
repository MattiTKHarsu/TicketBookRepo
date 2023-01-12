using System;
using System.Text.Json.Serialization;
using TicketBook.Controllers;

namespace TicketBook.Models
{
	public class Ticket
	{
        public int Id { get; set; }

        public int DeviceId { get; set; }
        public DateTime TimeStamp { get; set; }
        public String Description { get; set; } = string.Empty;
        public CategoryClass  Category { get; set; } 
        public StatusClass Status { get; set; }
    }
}

