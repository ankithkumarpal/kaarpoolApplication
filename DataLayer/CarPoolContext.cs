using Microsoft.EntityFrameworkCore;
using Models;

namespace DataLayer
{
    public class CarPoolContext : DbContext
    {

        public CarPoolContext(DbContextOptions<CarPoolContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<BookedRide> BookRides { get; set; }
        public DbSet<RideDetails> RideDetails { get; set; }
        public DbSet<Location> Locations { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                  //=> optionsBuilder.UseSqlServer("Server=tcp:carpoolserver.database.windows.net,1433;Initial Catalog=carpool;Persist Security Info=False;User ID=ankithpal;Password=Ankithpal@721;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    }
}
