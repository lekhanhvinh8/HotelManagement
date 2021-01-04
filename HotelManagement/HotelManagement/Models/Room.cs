namespace HotelManagement.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Room
    {
        public Room()
        {
            this.RoomRentalSlips = new HashSet<RoomRentalSlip>();
        }

        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public string Note { get; set; }
        public int RoomCategoryId { get; set; }

        public virtual RoomCategory RoomCategory { get; set; }
        public virtual ICollection<RoomRentalSlip> RoomRentalSlips { get; set; }
    }
}
