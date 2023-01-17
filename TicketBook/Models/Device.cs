//-----------------------------------------------------------------------------
//   -- T I C K E T   R E P O S I T O R Y   E X A M P L E   P R O G R A M --
//   
//      Company:        Etteplan Oy
//   	
//      Programmer:     Matti Harsu
//
//      Function:       Datamodel for Devices 
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
using TicketBook.Controllers;

namespace TicketBook.Models
{
	public class Device
	{
        public int Id { get; set; } = default;
        public string Name { get; set; } = String.Empty;
        public int Year { get; set; }
        public string Type { get; set; } = String.Empty;
    }
}

