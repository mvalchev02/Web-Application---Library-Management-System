using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class ReaderBookDbContext : DbContext
    {
        private string connString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\MyPC\\Downloads\\Documents\\Library.mdf;Integrated Security=True;Connect Timeout=30";

        public DbSet<ReaderBook> ReaderBook { get; set; }

        public ReaderBookDbContext()
        {
        }
        public ReaderBookDbContext(DbContextOptions<ReaderBookDbContext> options)
: base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connString);
        }
    }
}
