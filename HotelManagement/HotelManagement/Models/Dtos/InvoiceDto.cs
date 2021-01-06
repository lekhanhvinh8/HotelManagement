using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace HotelManagement.Models.Dtos
{
    public class InvoiceDto
    {
        public InvoiceDto()
        {
            var ticks = DateTime.Now.Ticks;
            var guid = Guid.NewGuid().ToString();

            this.ID = ticks.ToString() + guid;

            this.DateOfInvoice = ConvertDateToString(DateTime.Now);

            this.RoomRentalSlipDtos = new List<RoomRentalSlipDto>();
        }

        public InvoiceDto(Invoice invoice)
        {
            this.ID = invoice.ID;
            this.GuestId = invoice.GuestId;
            this.DateOfInvoice = ConvertDateToString(invoice.DateOfInvoice);
            this.TotalCost = invoice.TotalCost;
            this.GuestName = invoice.Guest.Name;

            this.RoomRentalSlipDtos = new List<RoomRentalSlipDto>();

            foreach (var roomRentalSlip in invoice.RoomRentalSlips)
            {
                this.RoomRentalSlipDtos.Add(new RoomRentalSlipDto(roomRentalSlip));
            }
        }

        public string ID { get; set; }
        public int GuestId { get; set; }

        public string GuestName { get; set; }
        public string DateOfInvoice { get; set; }
        public Nullable<float> TotalCost { get; set; }

        public List<RoomRentalSlipDto> RoomRentalSlipDtos { get; set; }
        public static string ConvertDateToString(DateTime dateTime)
        {
            //convert to mm/dd/yyyy
            var dateStr = dateTime.ToString("MM/dd/yyyy");

            return dateStr;
        }

        public static DateTime ConvertStringToDate(string date)
        {
            //convert mm/dd/yyyy to datetime
            var dateTime = DateTime.ParseExact(date, "MM/dd/yyyy", CultureInfo.InvariantCulture);

            return dateTime;
        }
    }
}