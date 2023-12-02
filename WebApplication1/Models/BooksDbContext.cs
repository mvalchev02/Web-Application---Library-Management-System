﻿using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class BooksDbContext : DbContext
    {
        private string connString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\MyPC\\Downloads\\Documents\\Library.mdf;Integrated Security=True;Connect Timeout=30";

        public DbSet<Books> Books { get; set; }

        public BooksDbContext()
        {
        }
        public BooksDbContext(DbContextOptions<BooksDbContext> options)
: base(options)
        { 
        
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connString);
        }
    }
}
