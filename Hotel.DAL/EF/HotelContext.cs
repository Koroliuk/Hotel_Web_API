using Hotel.DAL.Entities;
using System.Data.Entity;

namespace Hotel.DAL.EF
{
    public sealed class HotelContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomCategory> RoomCategories { get; set; }

        static HotelContext()
        {
            Database.SetInitializer<HotelContext>(new HotelContextInitializer());
        }

        public HotelContext() : base("DBConnection")
        {
        }
    }
}
