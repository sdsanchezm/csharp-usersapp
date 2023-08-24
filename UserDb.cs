using UserStore.Models;
using Microsoft.EntityFrameworkCore;

namespace UserStore.Data;

public class UserDb : DbContext
{
    public UserDb(DbContextOptions options) : base(options) { }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating (ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Firstname = "Jamecho", Lastname = "Sanc"}
        );
    }
}
