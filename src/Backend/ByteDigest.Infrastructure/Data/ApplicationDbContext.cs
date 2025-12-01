using ByteDigest.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ByteDigest.Infrastructure.Data;

/// <summary>
/// Represents the application database context.
/// </summary>
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
    /// </summary>
    /// <param name="options">The database context options.</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the Posts DbSet.
    /// </summary>
    public DbSet<Post> Posts { get; set; }

    /// <summary>
    /// Gets or sets the Pages DbSet.
    /// </summary>
    public DbSet<Page> Pages { get; set; }

    /// <summary>
    /// Configures the model relationships and constraints.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.HasIndex(p => p.Slug).IsUnique();
            entity.Property(p => p.Slug).IsRequired().HasMaxLength(200);
            entity.Property(p => p.Title).IsRequired().HasMaxLength(500);
            entity.Property(p => p.Excerpt).HasMaxLength(1000);
            entity.Property(p => p.Content).IsRequired();
            entity.Property(p => p.Category).HasMaxLength(100);
            entity.Property(p => p.Tags).HasMaxLength(500);
            entity.Property(p => p.CreatedAt).IsRequired();
        });

        modelBuilder.Entity<Page>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.HasIndex(p => p.Slug).IsUnique();
            entity.Property(p => p.Slug).IsRequired().HasMaxLength(200);
            entity.Property(p => p.Title).IsRequired().HasMaxLength(500);
            entity.Property(p => p.Content).IsRequired();
            entity.Property(p => p.CreatedAt).IsRequired();
        });
    }
}
