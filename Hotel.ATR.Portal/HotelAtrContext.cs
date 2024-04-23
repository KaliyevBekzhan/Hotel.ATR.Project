using Hotel.ATR.Portal.Models;
using Microsoft.EntityFrameworkCore;

namespace Hotel.ATR.Portal
{
    public class HotelAtrContext : DbContext
    {
        public HotelAtrContext(DbContextOptions<HotelAtrContext> options) : base(options)
        {
            
        }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<RoomData> RoomData { get; set; }
    }
}
