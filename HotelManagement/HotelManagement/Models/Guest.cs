namespace HotelManagement.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Guest
    {
        public Guest()
        {
            this.Invoices = new HashSet<Invoice>();
            this.RoomRentalSlips = new HashSet<RoomRentalSlip>();
        }

        public int Id { get; set; }
        public int CMND { get; set; }
        public string Name { get; set; }
        public bool Sex { get; set; }
        public string Address { get; set; }
        public int GuestCategoryId { get; set; }

        public virtual GuestCategory GuestCategory { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<RoomRentalSlip> RoomRentalSlips { get; set; }
    }
}
