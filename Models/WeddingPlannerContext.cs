#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
namespace WeddingPlanner.Models;
// the ProjectNameContext class representing a session with our MySQL database, allowing us to query for or save data
public class WeddingPlannerContext : DbContext 
{ 
    public WeddingPlannerContext(DbContextOptions options) : base(options) { }

    // the "TableName" table name will come from the DbSet property name
    // add DbSet<> properties below
    public DbSet<User> Users { get; set; } 
    public DbSet<Wedding> Weddings { get; set; } 
    public DbSet<UserWeddingAttend> Attends { get; set; } 
}