using HotelManagement.Models.Dtos;
using HotelManagement.Models.CustomValidations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.Models.ViewModels
{
    public class LoginViewModel
    {
        public AccountDto AccountDto { get; set; }

        [ValidRoleLogin]
        public int RoleID { get; set; }
    }
}