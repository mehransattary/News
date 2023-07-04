using Microsoft.EntityFrameworkCore;
using Entities.Models;

namespace Data.Context;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> option) : base(option)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<News> News { get; set; }
    public DbSet<Slider> Sliders { get; set; }
    public DbSet<Log> Logs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasOne(x=>x.Role).WithMany(x=>x.Users)
        .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Role>().HasMany(x=>x.Users).WithOne(x=>x.Role)
        .OnDelete(DeleteBehavior.NoAction);
        
        base.OnModelCreating(modelBuilder);
    }


   

}
