using HotelManagement.Models;
using PagedList;
using System;
using System.Collections.Generic;
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

        public ActionResult RoomRentalSlip(int page = 1, int pagesize = 10)
        {
            IEnumerable<RoomRentalSlip> listRoomRentalSlip = _context.RoomRentalSlips.OrderBy(x => x.Id).ToPagedList(page, pagesize);
            return View(listRoomRentalSlip);
        }

        public ActionResult InvoiceManagement()
        {
            return View();
        }
    }
}