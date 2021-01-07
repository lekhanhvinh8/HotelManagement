using HotelManagement.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelManagement.Controllers
{
    public class ManagerController : Controller
    {
        // GET: Manager
        private HotelManagementDbContext _context = null;

        public ManagerController()
        {
            _context = new HotelManagementDbContext();
        }
        
        public ActionResult MakeRoomRental()
        {
            return View();
        }

        public ActionResult RoomRentalSlip(int page = 1, int pagesize = 5)
        {
            IEnumerable<RoomRentalSlip> listRoomRentalSlip = _context.RoomRentalSlips.OrderBy(x => x.Id).ToPagedList(page, pagesize);
            return View(listRoomRentalSlip);
        }

        [HttpGet]
        public ActionResult CheckRoom(string room)
        {
            var roomNum = (from i in _context.Rooms
                           where i.RoomNumber == room
                           select i).SingleOrDefault();

            var roomRental = (from j in _context.RoomRentalSlips
                              where j.Room.RoomNumber == room
                              select j).SingleOrDefault();

            if (roomNum == null || roomRental == null)
            {
                return Content("false");
            }
            return Content("CreateInvoice?room=" + roomNum.Id.ToString());
        }

        //public ActionResult AddRoomRentalSlip(int room)
        //{
        //    var roomRentalSlip = (from i in _context.RoomRentalSlips
        //                          where i.RoomId == room
        //                          orderby i.Id
        //                          select i).SingleOrDefault();
        //    var listRoomRentalSlip = new List<RoomRentalSlip>();
        //    listRoomRentalSlip.Add(roomRentalSlip);
        //    return View(listRoomRentalSlip);
        //}

        
        public ActionResult RawInvoice()
        {
            IEnumerable<RoomRentalSlip> listRoomRentalSlip = (from i in _context.RoomRentalSlips
                                                              where i.Status == true
                                                              orderby i.Id
                                                              select i).ToPagedList(1, 5);
            return View("CreateInvoice", listRoomRentalSlip);
        }

        
        public ActionResult CreateInvoice(int room)
        {
            var roomRental = (from i in _context.RoomRentalSlips
                              where i.RoomId == room
                              where i.Status == false
                              select i).SingleOrDefault();
            if (roomRental == null)
            {
                ModelState.AddModelError("", "Error");
            }
            else
            {
                roomRental.Status = true;
                _context.RoomRentalSlips.AddOrUpdate(roomRental);
                _context.SaveChanges();
            }
           
            IEnumerable<RoomRentalSlip> listRoomRentalSlip = (from i in _context.RoomRentalSlips
                                                              where i.Status == true
                                                              orderby i.Id
                                                              select i).ToPagedList(1, 5);
            return View(listRoomRentalSlip);
        }

        public ActionResult InvoiceOfGuest(int icOfGuest, string nameOfGuest, string addressOfGuest, bool genderOfGuest)
        {
            try
            {
                var guestInDb = (from i in _context.Guests
                                 where i.CMND == icOfGuest
                                 select i).SingleOrDefault();

                if (guestInDb == null)
                {
                    var newGuest = new Guest();
                    newGuest.CMND = icOfGuest;
                    newGuest.Name = nameOfGuest;
                    newGuest.Address = addressOfGuest;
                    newGuest.Sex = genderOfGuest;
                    _context.Guests.Add(newGuest);
                    _context.SaveChanges();
                    return Content("true");
                }
                else
                {
                    guestInDb.Name = nameOfGuest;
                    guestInDb.Address = addressOfGuest;
                    guestInDb.Sex = genderOfGuest;
                    _context.Guests.AddOrUpdate(guestInDb);
                    _context.SaveChanges();
                    return Content("true");
                }
            }
            catch
            {

                return Content("false");
            }
        }

        [HttpPost]
        public ActionResult RemoveLine(int id)
        {
            var roomRental = (from i in _context.RoomRentalSlips
                              where i.Id == id
                              select i).SingleOrDefault();
            if (roomRental != null)
            {
                roomRental.Status = false;
                _context.RoomRentalSlips.AddOrUpdate(roomRental);
                _context.SaveChanges();
                return Content("/Manager/RawInvoice");
            }     
            return Content("false");
        }
        public ActionResult InvoiceManagement()
        {
            return View();
        }

        public ActionResult Statistical()
        {
            return View();
        }
    }
}