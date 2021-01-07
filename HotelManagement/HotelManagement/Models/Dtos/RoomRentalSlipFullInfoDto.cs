using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.Models.Dtos
{
    public class RoomRentalSlipFullInfoDto
    {
        public RoomRentalSlipFullInfoDto(RoomRentalSlip roomRentalSlip)
        {
            this.Id = roomRentalSlip.Id;
            this.RoomId = roomRentalSlip.RoomId;
            this.roomNumber = roomRentalSlip.Room.RoomNumber;
            this.roomCategory = roomRentalSlip.Room.RoomCategory.Name;
            this.StartDate = InvoiceDto.ConvertDateToString(roomRentalSlip.StartDate);
            this.EndDate = InvoiceDto.ConvertDateToString(roomRentalSlip.EndDate);

            this.HighestGuestCoefficient = roomRentalSlip.Guests.ToList()[0].GuestCategory.Coefficient;
            this.HighestGuestCoefficientName = roomRentalSlip.Guests.ToList()[0].GuestCategory.Name;
            foreach (var guest in roomRentalSlip.Guests)
            {
                if (this.HighestGuestCoefficient < guest.GuestCategory.Coefficient)
                {
                    this.HighestGuestCoefficient = guest.GuestCategory.Coefficient;
                    this.HighestGuestCoefficientName = guest.GuestCategory.Name;
                }
            }
            this.UnitPrice = roomRentalSlip.Room.RoomCategory.UnitPrice;
            this.NumStartSurcharge = roomRentalSlip.Room.RoomCategory.NumStartSurcharge;
            this.SurchargeRate = roomRentalSlip.Room.RoomCategory.SurchargeRate;

            this.Guests = new List<GuestDto>();

            foreach (var guest in roomRentalSlip.Guests)
            {
                this.Guests.Add(new GuestDto(guest));
            }

            this.Days = CalculationDifferentBetween2Days(StartDate, EndDate);

            if (this.Guests.Count < this.NumStartSurcharge)
                this.TotalCost = HighestGuestCoefficient * UnitPrice * Days;
            else
                this.TotalCost = HighestGuestCoefficient * UnitPrice * Days + UnitPrice * this.SurchargeRate;
        }
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string roomNumber { get; set; }
        public string  roomCategory { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public float UnitPrice { get; set; }
        public float HighestGuestCoefficient { get; set; }
        public string HighestGuestCoefficientName { get; set; }
        public int NumStartSurcharge { get; set; }
        public float SurchargeRate { get; set; }
        public int Days { get; set; }
        public float TotalCost { get; set; }
        public List<GuestDto> Guests { get; set; }

        private int CalculationDifferentBetween2Days(string startDateStr, string endDateStr)
        {
            var startDate = InvoiceDto.ConvertStringToDate(startDateStr);
            var endDate = InvoiceDto.ConvertStringToDate(endDateStr);

            var totalDays = (endDate - startDate).Days;

            return totalDays;
        }
    }
}