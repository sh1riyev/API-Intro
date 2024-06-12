using System;
using API_Intro.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Intro.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<Book> Books { get; set; }
		public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "UI UX",
                    CreateDate = DateTime.Now
                },
                   new Category
                   {
                       Id = 2,
                       Name = "Design",
                       CreateDate = DateTime.Now
                   },
                      new Category
                      {
                          Id = 3,
                          Name = "Development",
                          CreateDate = DateTime.Now
                      });


            base.OnModelCreating(modelBuilder);
        }
    }
}

