using System;
using System.Collections.Generic;

namespace Hotel.Atr.WebApi.Models
{
    public partial class Room
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string PathToImage { get; set; } = null!;
        public int? RoomDataid { get; set; }

        public virtual RoomDatum? RoomData { get; set; }
    }
}
