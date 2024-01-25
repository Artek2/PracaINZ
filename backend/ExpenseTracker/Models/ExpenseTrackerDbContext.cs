using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.Entities.Abstraction;

namespace Models
{
  public class ExpenseTrackerDbContext : DbContext
  {
    public ExpenseTrackerDbContext(DbContextOptions<ExpenseTrackerDbContext> options) : base(options)
    {

    }
    public DbSet<IncomeExpense> IncomeExpense { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<User>(entity =>
      {
        entity.Property(u => u.Email)
          .IsRequired();

        entity.HasMany(u => u.IncomeExpense)
        .WithOne(u => u.User)
        .HasForeignKey(c => c.UserId);

      });
    }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
      foreach (var entry in ChangeTracker.Entries<BaseEntity>())
      {
        switch (entry.State)
        {
          case EntityState.Added:
            entry.Entity.CreatedAt = DateTime.UtcNow;
            entry.Entity.UpdatedAt = null;
            break;
          case EntityState.Modified:
            entry.Entity.UpdatedAt = DateTime.UtcNow;
            break;
        }
      }

      return await base.SaveChangesAsync(cancellationToken);
    }
  }
}
