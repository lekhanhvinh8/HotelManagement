using HotelManagement.Models;
using HotelManagement.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HotelManagement.Controllers.api
{
    public class InvoicesController : ApiController
    {
        HotelManagementDbContext _context;

        public InvoicesController()
        {
            this._context = new HotelManagementDbContext();
        }
        
        [HttpGet]
        public List<InvoiceDto> GetAll()
        {
            var invoices = this._context.Invoices.OrderByDescending(i => i.DateOfInvoice).ToList();

            var invoiceDtos = new List<InvoiceDto>();

            foreach (var invoice in invoices)
            {
                invoiceDtos.Add(new InvoiceDto(invoice));
            }

            return invoiceDtos;
        }

        [HttpGet]
        public List<InvoiceDto> GetSome(int amount)
        {
            var invoices = this._context.Invoices.OrderByDescending(i => i.DateOfInvoice).Take(amount);

            var invoiceDtos = new List<InvoiceDto>();

            foreach (var invoice in invoices)
            {
                invoiceDtos.Add(new InvoiceDto(invoice));
            }

            return invoiceDtos;
        }

        [HttpGet]
        public List<InvoiceDto> GetSome(string startDate, string endDate, int roomCategoryID = -1, int roomID = -1)
        {
            //startDate and endDate must in format MM/dd/yy

            if (String.IsNullOrEmpty(startDate))
                startDate = InvoiceDto.ConvertDateToString(DateTime.MinValue);

            if (String.IsNullOrEmpty(endDate))
                endDate = InvoiceDto.ConvertDateToString(DateTime.Now);

            var invoices = this._context.Invoices.AsEnumerable().Where(i => (isBetween(InvoiceDto.ConvertDateToString(i.DateOfInvoice), startDate, endDate)) &&
                                                                            (isRentalRoomCategory(i.RoomRentalSlips, roomCategoryID) || (roomCategoryID == -1)) &&
                                                                            (isRentalRoom(i.RoomRentalSlips, roomID) || (roomID == -1))).ToList();

            var invoiceDtos = new List<InvoiceDto>();

            foreach (var invoice in invoices)
            {
                invoiceDtos.Add(new InvoiceDto(invoice));
            }

            return invoiceDtos;
        }

        [HttpGet]
        public List<InvoiceDto> GetSome(string id)
        {
            var invoice = this._context.Invoices.Find(id);

            var invoiceDtos = new List<InvoiceDto>();

            if (invoice != null)
                invoiceDtos.Add(new InvoiceDto(invoice));

            return invoiceDtos;
        }

        [HttpGet]
        public InvoiceDto Get(string id)
        {
            var invoice = this._context.Invoices.Find(id);

            if (invoice == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return new InvoiceDto(invoice);
        }

        [HttpGet]
        public InvoiceFullInfoDto GetFullInfo(string id)
        {
            var invoice = this._context.Invoices.Find(id);

            if(invoice == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return new InvoiceFullInfoDto(invoice);
        }
        private bool isBetween(string dateStr, string startDateStr, string endDateStr)
        {
            var date = InvoiceDto.ConvertStringToDate(dateStr);
            var startDate = InvoiceDto.ConvertStringToDate(startDateStr);
            var endDate = InvoiceDto.ConvertStringToDate(endDateStr);

            if (date.Ticks < startDate.Ticks || date.Ticks > endDate.Ticks)
                return false;

            return true;
        }

        private bool isRentalRoomCategory(List<RoomRentalSlip> roomRentalSlips, int roomCategoryID)
        {
            foreach (var roomRentalSlip in roomRentalSlips)
            {
                if (roomRentalSlip.Room.RoomCategoryId == roomCategoryID)
                    return true;
            }

            return false;
        }

        private bool isRentalRoom(List<RoomRentalSlip> roomRentalSlips, int roomID)
        {
            foreach (var roomRentalSlip in roomRentalSlips)
            {
                if (roomRentalSlip.Room.Id == roomID)
                    return true;
            }

            return false;
        }
    }
}
