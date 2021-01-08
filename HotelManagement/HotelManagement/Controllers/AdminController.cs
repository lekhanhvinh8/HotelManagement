using HotelManagement.Models.Consts;
using HotelManagement.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelManagement.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            if (!CheckLoginForAdmin())
                return RedirectToAction("Login", "Account", new { roleID = RoleIds.Admin });

            return View();
        }

        public ActionResult RevenueAreaChart()
        {
            return View();
        }

        private bool CheckLoginForAdmin()
        {
            if (Session[SessionNames.AdminID] == null)
                return false;

            return true;
        }
    }
}