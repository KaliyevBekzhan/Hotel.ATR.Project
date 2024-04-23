using Hotel.Atr.WebApi.Data;
using Hotel.Atr.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;

namespace Hotel.Atr.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private HotelAtrContext _db;
        public RoomController(HotelAtrContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IEnumerable<Room> Get()
        {
            var rooms = _db.Rooms;

            return rooms;
        }

        [HttpGet("{id}")]
        public Room Get(int id)
        {
            var room = _db.Rooms.FirstOrDefault(f => f.Id == id);

            return room;
        }

        [HttpGet("GetAvailableRoom")]
        public IEnumerable<Room> GetAvailableRoom()
        {
            var rooms = _db.Rooms;

            return rooms;
        }

        [HttpPost]
        public Room Post([FromBody] Room room)
        {
            _db.Rooms.Add(room);
            _db.SaveChanges();

            return room;
        }

        [HttpPut]
        public StatusCodeResult Put([FromForm] Room room)
        {
            var data = _db.Rooms.FirstOrDefault(f => f.Id == room.Id);
            if(data != null)
            {
                data.Name = room.Name;
                data.Price = room.Price;
                data.PathToImage = room.PathToImage;
                data.Description = room.Description;

                _db.Rooms.Update(data);
                _db.SaveChanges();

                return Ok();
            }

            return NotFound();
        }
        [HttpDelete]
        public StatusCodeResult Delete(int id)
        {
            var data = _db.Rooms.FirstOrDefault(f => f.Id == id);
            if (data != null)
            {
                _db.Rooms.Remove(data);
                _db.SaveChanges();
                return Ok();
            }
            return NotFound();
        }

    }
}
