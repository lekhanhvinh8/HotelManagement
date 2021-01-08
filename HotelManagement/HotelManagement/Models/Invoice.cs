namespace HotelManagement.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Invoice
    {
        public Invoice()
        {
            var ticks = DateTime.Now.Ticks;
            var guid = Guid.NewGuid().ToString();

            this.ID = ticks.ToString() + guid;
            this.RoomRentalSlips = new List<RoomRentalSlip>();
        }
        public Invoice(List<RoomRentalSlip> roomRentalSlips)
        {
            var ticks = DateTime.Now.Ticks;
            var guid = Guid.NewGuid().ToString();

            this.ID = ticks.ToString() + guid;

            if (roomRentalSlips == null)
                throw new Exception("Invoice must has at least 1 roomRentalSlip");

            if (roomRentalSlips.Count == 0)
                throw new Exception("Invoice must has at least 1 roomRentalSlip");

            this.RoomRentalSlips = roomRentalSlips;

            this.DateOfInvoice = DateTime.Now;
        }
        public string ID { get; set; }
        public int GuestId { get; set; }
        public System.DateTime DateOfInvoice { get; set; }
        public Nullable<float> TotalCost { get; set; }
        public virtual Guest Guest { get; set; }
        public virtual List<RoomRentalSlip> RoomRentalSlips { get; set; }
    }
}
