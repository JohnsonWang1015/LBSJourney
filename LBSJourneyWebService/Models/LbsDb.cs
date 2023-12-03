using LBSJourneyWebService.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace LBSJourneyWebService.Models;

public class LbsDb:DbContext
{
    public LbsDb(DbContextOptions<LbsDb> options):base(options)
    {
        Console.WriteLine($"DbContext 被運行...");
    }
    public DbSet<Places> Places { get; set; }
    public DbSet<Users> Users { get; set; }
    public DbSet<Locations> Locations { get; set; }
}