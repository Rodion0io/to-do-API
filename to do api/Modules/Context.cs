using System;
using Microsoft.EntityFrameworkCore;

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

