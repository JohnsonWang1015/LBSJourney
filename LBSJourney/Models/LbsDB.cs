using LBSJourney.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace LBSJourney.Models;

public class LbsDB:DbContext
{
    public LbsDB(DbContextOptions<LbsDB> options) : base(options)
    {
        Console.WriteLine($"DbContext 被運行...");
    }
    public DbSet<Users> Users { get; set; }
    public DbSet<Places> Places { get; set; }
    public DbSet<Favorites> Favorites { get; set; }
    public DbSet<Locations> Locations { get; set; }
    public DbSet<Reviews> Reviews { get; set; }
}