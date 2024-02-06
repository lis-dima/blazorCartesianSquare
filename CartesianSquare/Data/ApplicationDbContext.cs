using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CartesianSquare.Shared.Records;
using CartesianSquare.Shared.Squares;

namespace CartesianSquare.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Record>()
                .HasOne(r => r.Square) 
                .WithMany(s => s.Records) 
                .HasForeignKey(r => r.SquareId);

        }
        public DbSet<Square> Squares { get; set; } = default!;
        public DbSet<Record> Records { get; set; } = default!;
    }
}
