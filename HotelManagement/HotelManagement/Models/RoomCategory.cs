using System;
using System.Collections.Generic;

namespace HotelManagement.Models
{
    public class RoomCategory
    {
        public RoomCategory()
        {
            this.Rooms = new HashSet<Room>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public float UnitPrice { get; set; }
        public int MaxNumberOfGuests { get; set; }
        public int NumStartSurcharge { get; set; }
        public float SurchargeRate { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}