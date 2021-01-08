using HotelManagement.Models.Consts;
using HotelManagement.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelManagement.Models.CustomValidations
{
    public class ValidRoleLogin : ValidationAttribute
    {
        private HotelManagementDbContext _context;
        public ValidRoleLogin()
        {
            this._context = new HotelManagementDbContext();
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var loginViewModel = (LoginViewModel)validationContext.ObjectInstance;

            var accountDto = loginViewModel.AccountDto;

            if (String.IsNullOrEmpty(accountDto.Username) || String.IsNullOrEmpty(accountDto.Password))
                return ValidationResult.Success;

            var result = Login(accountDto.Username, accountDto.Password);

            if (result == 0)
                return new ValidationResult("Your account or your password is incorrect");

            var accountInDb = this._context.Accounts.SingleOrDefault(a => a.Username == accountDto.Username);

            if (accountInDb == null)
                throw new Exception("Not found");

            var roleID = accountInDb.Role.ID;

            if (roleID != loginViewModel.RoleID && loginViewModel.RoleID != RoleIds.Unknown)
                return new ValidationResult("You are not logged in with the correct role");

            return ValidationResult.Success;
        }

        private int Login(string username, string password)
        {
            string pwd = Syptop.Encrypt(password, true);
            var account = this._context.Accounts.SingleOrDefault(r => r.Username == username
                                                                 && r.PasswordHash == pwd);
            var res = 0;
            if (account != null)
            {
                res = account.RoleID;
            }

            return res;
        }
    }
}