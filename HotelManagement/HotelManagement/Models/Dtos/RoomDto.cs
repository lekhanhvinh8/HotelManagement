using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelManagement.Models.Dtos
{
    public class RoomDto
    {
        public RoomDto()
        {
        }
        public RoomDto(Room room)
        {
            this.id = room.id;
            this.room_number = room.room_number;
            this.note = room.note;
            this.room_category_id = room.room_category_id;
        }
        public int id { get; set; }

        [StringLength(10)]
        public string room_number { get; set; }
        public string note { get; set; }
        public int room_category_id { get; set; }

        public Room CreateModel()
        {
            var room = new Room();

            room.room_number = this.room_number;
            room.note = this.note;
            room.room_category_id = this.room_category_id;

            return room;
        }

        public Room Update(Room room)
        {
            room.room_number = this.room_number;
            room.note = this.note;
            room.room_category_id = this.room_category_id;

            return room;
        }
    }
}