//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HotelManagement.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Bill
    {
        public int room_rental_slip_id { get; set; }
        public int guest_id { get; set; }
        public System.DateTime date_of_invoice { get; set; }
        public Nullable<float> total_cost { get; set; }
    
        public virtual Guest Guest { get; set; }
        public virtual Room_rental_slip Room_rental_slip { get; set; }
    }
}