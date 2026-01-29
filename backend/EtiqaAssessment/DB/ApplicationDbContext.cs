using Microsoft.EntityFrameworkCore;

namespace EtiqaAssessment.DB;

public class ApplicationDbContext : DbContext
{
    // Constructor to pass configuration options
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // DbSet properties for your entities
    public DbSet<Employees> Employees { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Employees>(entity =>
        {
            entity.HasKey(e => e.ID);

            // Optional: constrain string lengths to match your schema
            entity.Property(e => e.FirstName).HasMaxLength(200);
            entity.Property(e => e.LastName).HasMaxLength(200);
            entity.Property(e => e.WorkingDays).HasMaxLength(5);

            // Use HasPrecision for provider-independent precision/scale
            entity.Property(e => e.DailyRate).HasPrecision(18, 2);
            entity.Property(e => e.TakeHomePay).HasPrecision(18, 2);

            // Optional: map date-only columns explicitly
            entity.Property(e => e.DateOfBirth).HasColumnType("date");
            entity.Property(e => e.StartDate).HasColumnType("date");
            entity.Property(e => e.EndDate).HasColumnType("date");
        });
    }
}