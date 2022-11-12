using Microsoft.EntityFrameworkCore;

namespace Add_Database_Model.Models
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext>  options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
           
        }

        public DbSet<Genre>  Genres { get; set; }
        public DbSet<Movie>  Movies  { get; set; }

    }
}
