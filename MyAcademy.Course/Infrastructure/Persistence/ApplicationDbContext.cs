using Microsoft.EntityFrameworkCore;
using MyAcademy.Course.Domain;

namespace MyAcademy.Course.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public DbSet<Domain.Course> Courses { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Chapter> Chapters { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}