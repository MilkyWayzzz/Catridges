using Catridges.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Catridges.DAL;

public class ApplicationDbContext : DbContext
{
    public DbSet<Catridge> Catridge { get; set; }
    public DbSet<State> States { get; set; }
    
    public DbSet<Printer> Printers { get; set; }
    
    public DbSet<Subdivision> Subdivisions { get; set; }
    
    public DbSet<Document> Documents { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }
}