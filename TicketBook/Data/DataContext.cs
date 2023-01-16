//-----------------------------------------------------------------------------
//   -- T I C K E T   R E P O S I T O R Y   E X A M P L E   P R O G R A M --
//   
//		Company: 		Etteplan Oy
//   	
//		Programmer: 	Matti Harsu
//
//      Function:       Database related Services
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
using Microsoft.EntityFrameworkCore;
using TicketBook.Models;

namespace TicketBook.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{
		}

		public DbSet<Ticket> Tickets { get; set; }
		public DbSet<Device> Devices { get; set; }
	}
}

