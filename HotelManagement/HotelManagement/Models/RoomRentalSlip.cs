namespace HotelManagement.Models
{
    using System;
    using System.Collections.Generic;

    public partial class RoomRentalSlip
    {
        public RoomRentalSlip()
        {
            this.Invoices = new HashSet<Invoice>();
            this.Guests = new HashSet<Guest>();
        }

        public int Id { get; set; }
        public int RoomId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual Room Room { get; set; }
        public virtual ICollection<Guest> Guests { get; set; }
    }
}
