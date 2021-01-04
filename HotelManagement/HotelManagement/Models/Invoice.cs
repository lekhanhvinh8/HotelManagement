namespace HotelManagement.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Invoice
    {
        public int InvoiceID { get; set; }
        public int RoomRentalSlipId { get; set; }
        public int GuestId { get; set; }
        public System.DateTime DateOfInvoice { get; set; }
        public Nullable<float> TotalCost { get; set; }
        public virtual Guest Guest { get; set; }
        public virtual RoomRentalSlip RoomRentalSlip { get; set; }
    }
}
