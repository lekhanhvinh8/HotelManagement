using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.Models
{
    public class Role
    {
        public Role()
        {
            this.Accounts = new List<Account>();
        }
        public int ID { get; set; }
        public string RoleName { get; set; }
        public string Descriptions { get; set; }

        public List<Account> Accounts { get; set; }
    }
}