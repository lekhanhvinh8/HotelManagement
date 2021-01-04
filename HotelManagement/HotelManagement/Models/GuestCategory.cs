namespace HotelManagement.Models
{
    using System;
    using System.Collections.Generic;

    public partial class GuestCategory
    {
        public GuestCategory()
        {
            this.Guests = new HashSet<Guest>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public float Coefficient { get; set; }
        public virtual ICollection<Guest> Guests { get; set; }
    }
}
