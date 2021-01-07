using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace HotelManagement.Models.Dtos
{
    public class RoomRentalSlipDto
    {
        public RoomRentalSlipDto()
        {
            this.Guests = new List<GuestDto>();
        }

        public RoomRentalSlipDto(int roomId, string dateStart, string dateEnd)
        {
            this.Guests = new List<GuestDto>();

            this.RoomId = roomId;
            this.StartDate = dateStart;
            this.EndDate = dateEnd;
        }

        public RoomRentalSlipDto(RoomRentalSlip roomRentalSlip)
        {
            this.Id = roomRentalSlip.Id;
            this.RoomId = roomRentalSlip.RoomId;
            this.StartDate = ConvertDateToString(roomRentalSlip.StartDate);
            this.EndDate = ConvertDateToString(roomRentalSlip.EndDate);

            this.Guests = new List<GuestDto>();

            foreach (var guest in roomRentalSlip.Guests)
            {
                this.Guests.Add(new GuestDto(guest));
            }
        }

        public RoomRentalSlip CreateModel()
        {
            var roomRentalSlip = new RoomRentalSlip(this.RoomId, this.ConvertStringToDate(this.StartDate), this.ConvertStringToDate(this.EndDate));

            foreach (var guestDto in this.Guests)
            {
                roomRentalSlip.Guests.Add(guestDto.CreateModel());
            }

            return roomRentalSlip;
        }

        public int Id { get; set; }
        public int RoomId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public List<GuestDto> Guests { get; set; }

        private string ConvertDateToString(DateTime dateTime)
        {
            //convert to mm/dd/yyyy
            var dateStr = dateTime.ToString("MM/dd/yyyy");
            
            return dateStr;
        }

        private DateTime ConvertStringToDate(string date)
        {
            //convert mm/dd/yyyy to datetime
            var dateTime = DateTime.ParseExact(date, "MM/dd/yyyy", CultureInfo.InvariantCulture);

            return dateTime;
        }


    }
}