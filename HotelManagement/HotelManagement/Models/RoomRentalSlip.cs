namespace HotelManagement.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
        public bool Status { get; set; }

        public float CalculateTotalCost()
        {
            float totalCost = 0;

            float highestGuestCoefficient = this.Guests.ToList()[0].GuestCategory.Coefficient;

            foreach (var guest in this.Guests)
            {
                if (highestGuestCoefficient < guest.GuestCategory.Coefficient)
                    highestGuestCoefficient = guest.GuestCategory.Coefficient;
            }

            float unitPrice = this.Room.RoomCategory.UnitPrice;
            int numStartSurcharge = this.Room.RoomCategory.NumStartSurcharge;
            float surchargeRate = this.Room.RoomCategory.SurchargeRate;

            var totalDays = (this.EndDate - this.StartDate).Days;

            if(this.Guests.Count < numStartSurcharge)
            {
                totalCost = unitPrice * highestGuestCoefficient * totalDays;
            }
            else
            {
                totalCost = unitPrice * highestGuestCoefficient * totalDays + unitPrice * surchargeRate;
            }

            return totalCost;
        }
    }
}
