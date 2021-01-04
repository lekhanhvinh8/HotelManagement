namespace HotelManagement.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Room
    {
        public Room()
        {
            this.RoomRentalSlips = new HashSet<RoomRentalSlip>();
            this.IsAvailable = true;
        }

        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public string Note { get; set; }
        public int RoomCategoryId { get; set; }
        public bool IsAvailable { get; set; }
        public virtual RoomCategory RoomCategory { get; set; }
        public virtual ICollection<RoomRentalSlip> RoomRentalSlips { get; set; }

        
    }
}
