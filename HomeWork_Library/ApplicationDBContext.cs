using Microsoft.EntityFrameworkCore;
using HomeWork_Library.models;


namespace HomeWork_Library
{
	public class ApplicationDBContext : DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder option)
		{
			option.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Library-HomeWork;Integrated Security=True");
		}

		public DbSet<User>? Users { get; set; }
		public DbSet<Book>? Books { get; set; }


        // secound method
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //	modelBuilder.Entity<User>();
        //}
    }
}
