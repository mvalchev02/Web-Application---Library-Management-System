using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class ReadersDbContext : DbContext
    {
        private string connString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\MyPC\\Downloads\\Documents\\Library.mdf;Integrated Security=True;Connect Timeout=30";

        public DbSet<Readers> Readers { get; set; }

        public ReadersDbContext()
        {
        }
        public ReadersDbContext(DbContextOptions<ReadersDbContext> options)
: base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connString);
        }
    }
}
