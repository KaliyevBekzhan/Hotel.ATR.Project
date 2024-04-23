using System.ComponentModel.DataAnnotations;

namespace Hotel.ATR.Portal.Models
{
    public class RoomData
    {
        [Key]
        public int id { get; set; }
        public List<Room> Rooms {  get; set; }
        public List<Client> Clients {  get; set; }
    }
}
