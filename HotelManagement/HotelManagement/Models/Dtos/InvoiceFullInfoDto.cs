using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.Models.Dtos
{
    public class InvoiceFullInfoDto
    {
        public InvoiceFullInfoDto(Invoice invoice)
        {
            this.ID = invoice.ID;
            this.GuestId = invoice.GuestId;
            this.GuestName = invoice.Guest.Name;
            this.GuestCMND = invoice.Guest.CMND;
            this.DateOfInvoice = InvoiceDto.ConvertDateToString(invoice.DateOfInvoice);
            this.TotalCost = 0;

            this.RoomRentalSlipFullInfoDtos = new List<RoomRentalSlipFullInfoDto>();

            foreach (var roomRentalSlip in invoice.RoomRentalSlips)
            {
                this.RoomRentalSlipFullInfoDtos.Add(new RoomRentalSlipFullInfoDto(roomRentalSlip));
            }

            foreach (var roomRentalSlip in this.RoomRentalSlipFullInfoDtos)
            {
                this.TotalCost += roomRentalSlip.TotalCost;
            }
        }
        public string ID { get; set; }
        public int GuestId { get; set; }
        public string GuestName { get; set; }
        public int GuestCMND { get; set; }
        public string DateOfInvoice { get; set; }
        public Nullable<float> TotalCost { get; set; }
        public List<RoomRentalSlipFullInfoDto> RoomRentalSlipFullInfoDtos { get; set; }
    }
}