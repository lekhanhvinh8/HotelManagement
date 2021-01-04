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
            this.Id = room.Id;
            this.RoomNumber = room.RoomNumber;
            this.Note = room.Note;
            this.RoomCategoryID = room.RoomCategoryId;
        }
        public int Id { get; set; }

        [StringLength(10)]
        public string RoomNumber { get; set; }
        public string Note { get; set; }
        public int RoomCategoryID { get; set; }

        public Room CreateModel()
        {
            var room = new Room();

            room.RoomNumber = this.RoomNumber;
            room.Note = this.Note;
            room.RoomCategoryId = this.RoomCategoryID;

            return room;
        }

        public Room Update(Room room)
        {
            room.RoomNumber = this.RoomNumber;
            room.Note = this.Note;
            room.RoomCategoryId = this.RoomCategoryID;

            return room;
        }
    }
}