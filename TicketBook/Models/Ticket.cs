//-----------------------------------------------------------------------------
//   -- T I C K E T   R E P O S I T O R Y   E X A M P L E   P R O G R A M --
//   
//      Company:        Etteplan Oy
//   	
//      Programmer:     Matti Harsu
//
//      Function:       Datamodel for Tickets
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

