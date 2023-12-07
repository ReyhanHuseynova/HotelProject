using HotelProjectEntity.Entity;
using Microsoft.EntityFrameworkCore;

namespace HotelProject.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {
            
        }
        public DbSet<Position>Positions { get; set; }
        public DbSet<Team>Teams { get; set; }
        public DbSet<Room>Rooms { get; set; }
        public DbSet<Service>Services { get; set; }
        public DbSet<Social>Socials { get; set; }
        public DbSet<Subscribe>Subscribes { get; set; }
        public DbSet<Booking>Bookings { get; set; }
    }
}
