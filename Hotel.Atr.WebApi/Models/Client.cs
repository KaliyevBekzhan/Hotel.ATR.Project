using System;
using System.Collections.Generic;

namespace Hotel.Atr.WebApi.Models
{
    public partial class Client
    {
        public int Id { get; set; }
        public string PathToImg { get; set; } = null!;
        public string PathToImgHover { get; set; } = null!;
        public int? RoomDataid { get; set; }

        public virtual RoomDatum? RoomData { get; set; }
    }
}
