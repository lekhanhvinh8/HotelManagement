namespace HotelManagement.Models
{
    using System;
    using System.Collections.Generic;

    public partial class RoomRentalSlip
    {
        public RoomRentalSlip()
        {
        }

        public RoomRentalSlip(int roomId, DateTime dateStart, DateTime dateEnd)
        {
            this.Guests = new HashSet<Guest>();

            this.RoomId = roomId;
            this.StartDate = dateStart;
            this.EndDate = dateEnd;
        }

        public int Id { get; set; }
        public int RoomId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string InvoiceID { get; set; }
        public virtual Invoice Invoice { get; set; }
        public virtual Room Room { get; set; }
        public virtual ICollection<Guest> Guests { get; set; }
        public float? LineTotal { get; set; }
    }
}
