using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.Models.Dtos
{
    public class GuestDto
    {
        public GuestDto()
        {

        }

        public GuestDto(Guest guest)
        {
            this.Id = guest.Id;
            this.CMND = guest.CMND;
            this.Name = guest.Name;
            this.Sex = guest.Sex;
            this.Address = guest.Address;
            this.GuestCategoryId = guest.GuestCategoryId;
        }

        public Guest CreateModel()
        {
            Guest guest = new Guest();
            guest.CMND = this.CMND;
            guest.Name = this.Name;
            guest.Sex = this.Sex;
            guest.Address = this.Address;
            guest.GuestCategoryId = this.GuestCategoryId;

            return guest;
        }

        public int Id { get; set; }
        public int CMND { get; set; }
        public string Name { get; set; }
        public bool Sex { get; set; }
        public string Address { get; set; }
        public int GuestCategoryId { get; set; }

    }
}