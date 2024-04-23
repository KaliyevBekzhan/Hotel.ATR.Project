using System;
using System.Collections.Generic;

namespace Hotel.Atr.WebApi.Models
{
    public partial class RoomDatum
    {
        public RoomDatum()
        {
            Clients = new HashSet<Client>();
            Rooms = new HashSet<Room>();
        }

        public int Id { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
