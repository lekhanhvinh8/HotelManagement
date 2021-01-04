using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.Models
{
    public class Account
    {
        public Account()
        {
                
        }
        public Account(string userName, string password)
        {
            this.Username = userName;
            this.PasswordHash = Syptop.Encrypt(password, true);
        }
        public int ID { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public int RoleID { get; set; }
        public virtual Role Role { get; set; }
    }
}