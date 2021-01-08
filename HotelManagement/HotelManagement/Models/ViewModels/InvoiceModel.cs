using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace HotelManagement.Models.ViewModels
{
    public class InvoiceModel
    {
        private HotelManagementDbContext _context = null;
        public InvoiceModel()
        {
            _context = new HotelManagementDbContext();
        }
        public bool AddGuest(Guest guest)
        {
            try
            {
                _context.Guests.Add(guest);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateGuest(Guest guest, string name, bool gender, string address, int cat)
        {
            try
            {
                guest.Name = name;
                guest.Sex = gender;
                guest.Address = address;
                guest.GuestCategoryId = cat;
                _context.Guests.AddOrUpdate(guest);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddInvoice(string invoiceID, int guestId, float totalCost)
        {
            try
            {
                var invoice = new Invoice();
                invoice.ID = invoiceID;
                invoice.GuestId = guestId;
                invoice.DateOfInvoice = DateTime.Now;
                invoice.TotalCost = totalCost;
                _context.Invoices.Add(invoice);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateRoomRentalSlip(RoomRentalSlip roomRentalSlip)
        {
            try
            {            
                _context.RoomRentalSlips.AddOrUpdate(roomRentalSlip);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateRoomStatus(Room room)
        {
            try
            {
                _context.Rooms.AddOrUpdate(room);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}