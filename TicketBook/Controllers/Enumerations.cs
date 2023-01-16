//-----------------------------------------------------------------------------
//   -- T I C K E T   R E P O S I T O R Y   E X A M P L E   P R O G R A M --
//   
//		Company: 		Etteplan Oy
//   	
//		Programmer: 	Matti Harsu
//
//      Function:       Constants etc.
//
//		Functionality:
//      This program work as Back-End part for recording servive request.
//      Application contains 1) Create record, 2) Read record, 3) Upadate
//      record and 4) Delete record function. In addition all service records
//      and records regarding defined devices can be listed.
//
//		Libraries:
//		- 
//
//-----------------------------------------------------------------------------

using System;
using System.Text.Json.Serialization;

namespace TicketBook.Controllers
{
    // Severity of the Fault
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CategoryClass
    {
        Critical = 1,
        Important = 2,
        Low = 3
    }

    // Status of the Fault
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StatusClass
    {
        Open = 1,
        Done = 2
    }
}