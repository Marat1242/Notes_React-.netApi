using Microsoft.EntityFrameworkCore;
using NotesPetProject.Models;

namespace NotesPetProject.Data
{
    public class NotesDbContext : DbContext
    {
        private readonly IConfiguration Configuration;
        public NotesDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Configuration.GetConnectionString("ConnectionString"));

        }
        public DbSet<Note> Notes => Set<Note>();
        


    }
}
