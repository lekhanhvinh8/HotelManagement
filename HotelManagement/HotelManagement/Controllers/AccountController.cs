using HotelManagement.Models;
using HotelManagement.Models.Consts;
using HotelManagement.Models.Dtos;
using HotelManagement.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HotelManagement.Controllers
{
    public class AccountController : Controller
    {
        private HotelManagementDbContext _context;
        public AccountController()
        {
            _context = new HotelManagementDbContext();
        }
        // GET: Account
        public ActionResult Login(int roleID = RoleIds.Unknown)
        {
            var loginViewModel = new LoginViewModel() { AccountDto = new AccountDto(), RoleID = roleID };

            return View(loginViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Login", loginViewModel);
            }

            var acc = loginViewModel.AccountDto;

            var result = Login(acc.Username, acc.Password);

            if (result == 0)
                return View(loginViewModel);

            var accInDb = this._context.Accounts.SingleOrDefault(a => a.Username == acc.Username);

            if (accInDb == null)
                throw new Exception("Not found");
           
            if (result == RoleIds.Admin)
            {
                Session[SessionNames.AdminID] = accInDb.ID;
                return RedirectToAction("Index", "Admin");
            }
            else if (result == RoleIds.Manager)
            {
                Session[SessionNames.ManagerID] = accInDb.ID;
                return RedirectToAction("MakeRoomRental", "Manager");
            }

            return View(loginViewModel);
        }
        public ActionResult Logout(string sessionName)
        {
            if (Session[sessionName] == null)
                throw new System.Web.Http.HttpResponseException(HttpStatusCode.NotFound);

            Session[sessionName] = null;

            return RedirectToAction("Login", "Account");
        }
        private int Login(string username, string password)
        {
            string pwd = Syptop.Encrypt(password, true);
            var account = this._context.Accounts.SingleOrDefault(r => r.Username == username
                                                                 && r.PasswordHash == pwd);
            var res = account.RoleID;

            return res;
        }
    }
}