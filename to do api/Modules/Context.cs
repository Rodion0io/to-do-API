using System;
using Microsoft.EntityFrameworkCore;


//Здесь образуется бд

namespace to_do_api.Modules
{
	public class Context : DbContext
	{
		public DbSet<Card> Cards { get; set; }

		public Context(DbContextOptions<Context> options) : base (options)
		{
			Database.EnsureCreated();

        }
	}
}

