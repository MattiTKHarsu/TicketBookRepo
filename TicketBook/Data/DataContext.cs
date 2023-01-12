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
	}
}

