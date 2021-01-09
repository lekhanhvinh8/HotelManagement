using HotelManagement.Models;
using HotelManagement.Models.Consts;
using HotelManagement.Models.ViewModels;
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
            if (!CheckLoginForManager())
                return RedirectToAction("Login", "Account", new { roleID = RoleIds.Manager });

            return View();
        }

        public ActionResult RoomRentalSlip(bool isPaid = false, int page = 1, int pagesize = 5)
        {
            if (!CheckLoginForManager())
                return RedirectToAction("Login", "Account", new { roleID = RoleIds.Manager });
            if (isPaid == false)
            {
                ViewBag.Data = 1;
                IEnumerable<RoomRentalSlip> listRoomRentalSlip = (from i in _context.RoomRentalSlips
                                                                  where i.InvoiceID == null
                                                                  orderby i.Id
                                                                  select i).ToPagedList(page, pagesize);
                return View(listRoomRentalSlip);
            }
            else
            {
                ViewBag.Data = 2;
                IEnumerable<RoomRentalSlip> listRoomRentalSlip = (from i in _context.RoomRentalSlips
                                                                  where i.InvoiceID != null
                                                                  orderby i.Id
                                                                  select i).ToPagedList(page, pagesize);
                return View(listRoomRentalSlip);
            }
        }

        [HttpGet]
        public ActionResult CheckRoom(string room)
        {
            var roomNum = (from i in _context.Rooms
                           where i.RoomNumber == room
                           select i).SingleOrDefault();

            var roomRental = (from j in _context.RoomRentalSlips
                              where j.Room.RoomNumber == room
                              where j.InvoiceID == null
                              select j).SingleOrDefault();

            if (roomNum == null || roomRental == null)
            {
                return Content("false");
            }
            return Content("CreateInvoice?room=" + roomNum.Id.ToString());
        }

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
            if (!CheckLoginForManager())
                return RedirectToAction("Login", "Account", new { roleID = RoleIds.Manager });

            var roomRental = (from i in _context.RoomRentalSlips
                              where i.RoomId == room
                              where i.Status == false
                              where i.InvoiceID == null
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
        
        public ActionResult InvoiceOfGuest(int icOfGuest, string nameOfGuest, string addressOfGuest, bool genderOfGuest, int CatOfGuest, float totalCost, string invoiceIDInput)
        {
            try
            {
                var guestInDb = (from i in _context.Guests
                                 where i.CMND == icOfGuest
                                 select i).FirstOrDefault();

                var roomRentalSlips = (from i in _context.RoomRentalSlips
                                       where i.Status == true
                                       select i).ToList();

                if (roomRentalSlips.Count == 0)
                {
                    return Content("noRoom");
                }

                string invoiceID;

                if (invoiceIDInput == null)
                    invoiceID = new GenerateInvoiceID().RandomString(10);
                else
                    invoiceID = invoiceIDInput;

                List<Room> room = new List<Room>();
                if (guestInDb == null)
                {
                    var guest = new Guest();                   
                    guest.CMND = icOfGuest;
                    guest.Name = nameOfGuest;
                    guest.Sex = genderOfGuest;
                    guest.Address = addressOfGuest;
                    guest.GuestCategoryId = CatOfGuest;
                    if (new InvoiceModel().AddGuest(guest))
                    {                        
                        if (new InvoiceModel().AddInvoice(invoiceID, guest.Id, totalCost))
                        {
                            
                            foreach (var item in roomRentalSlips)
                            {
                                item.InvoiceID = invoiceID;
                                item.Status = false;
                                room = (from i in _context.Rooms
                                            where i.Id == item.RoomId
                                            select i).ToList();
                                if (new InvoiceModel().UpdateRoomRentalSlip(item))
                                {
                                }
                            }
                            foreach (var item in room)
                            {
                                item.IsAvailable = true;
                                if(new InvoiceModel().UpdateRoomStatus(item))
                                {
                                }    
                            }                           
                        }
                    }
                    return Content("/Manager/InvoiceManagement");

                }
                else
                {
                    if (new InvoiceModel().UpdateGuest(guestInDb, nameOfGuest, genderOfGuest, addressOfGuest, CatOfGuest))
                    {
                        if (new InvoiceModel().AddInvoice(invoiceID ,guestInDb.Id, totalCost))
                        {
                            foreach (var item in roomRentalSlips)
                            {
                                item.InvoiceID = invoiceID;
                                item.Status = false;
                                room = (from i in _context.Rooms
                                        where i.Id == item.RoomId
                                        select i).ToList();
                                if (new InvoiceModel().UpdateRoomRentalSlip(item))
                                {
                                }
                            }
                            foreach (var item in room)
                            {
                                item.IsAvailable = true;
                                if (new InvoiceModel().UpdateRoomStatus(item))
                                {
                                }
                            }
                        }
                    }
                    return Content("/Manager/InvoiceManagement");
                }
            }
            catch
            {
                return Content("false");
            }
        }

        public void Test(int icOfGuest, string nameOfGuest, string addressOfGuest, bool genderOfGuest, int CatOfGuest, float totalCost, string invoiceIDInput)
        {
            try
            {
                var guestInDb = (from i in _context.Guests
                                 where i.CMND == icOfGuest
                                 select i).SingleOrDefault();

                var roomRentalSlips = (from i in _context.RoomRentalSlips
                                       where i.Status == true
                                       select i).ToList();

                if (roomRentalSlips.Count == 0)
                {
                    return;
                }

                string invoiceID;

                if (invoiceIDInput == null)
                    invoiceID = new GenerateInvoiceID().RandomString(10);
                else
                    invoiceID = invoiceIDInput;

                List<Room> room = new List<Room>();
                if (guestInDb == null)
                {
                    var guest = new Guest();
                    guest.CMND = icOfGuest;
                    guest.Name = nameOfGuest;
                    guest.Sex = genderOfGuest;
                    guest.Address = addressOfGuest;
                    guest.GuestCategoryId = CatOfGuest;
                    if (new InvoiceModel().AddGuest(guest))
                    {
                        if (new InvoiceModel().AddInvoice(invoiceID, guest.Id, totalCost))
                        {

                            foreach (var item in roomRentalSlips)
                            {
                                item.InvoiceID = invoiceID;
                                item.Status = false;
                                room = (from i in _context.Rooms
                                        where i.Id == item.RoomId
                                        select i).ToList();
                                if (new InvoiceModel().UpdateRoomRentalSlip(item))
                                {
                                }
                            }
                            foreach (var item in room)
                            {
                                item.IsAvailable = true;
                                if (new InvoiceModel().UpdateRoomStatus(item))
                                {
                                }
                            }
                        }
                    }
                    return;

                }
                else
                {
                    if (new InvoiceModel().UpdateGuest(guestInDb, nameOfGuest, genderOfGuest, addressOfGuest, CatOfGuest))
                    {
                        if (new InvoiceModel().AddInvoice(invoiceID, guestInDb.Id, totalCost))
                        {
                            foreach (var item in roomRentalSlips)
                            {
                                item.InvoiceID = invoiceID;
                                item.Status = false;
                                room = (from i in _context.Rooms
                                        where i.Id == item.RoomId
                                        select i).ToList();
                                if (new InvoiceModel().UpdateRoomRentalSlip(item))
                                {
                                }
                            }
                            foreach (var item in room)
                            {
                                item.IsAvailable = true;
                                if (new InvoiceModel().UpdateRoomStatus(item))
                                {
                                }
                            }
                        }
                    }
                    return;
                }
            }
            catch
            {
                return ;
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
            if (!CheckLoginForManager())
                return RedirectToAction("Login", "Account", new { roleID = RoleIds.Manager });
            return View();
        }

        private bool CheckLoginForManager()
        {
            if (Session[SessionNames.ManagerID] == null)
                return false;

            return true;
        }

        public ActionResult Statistical()
        {
            if (!CheckLoginForManager())
                return RedirectToAction("Login", "Account", new { roleID = RoleIds.Manager });

            return View();
        }
    }
}