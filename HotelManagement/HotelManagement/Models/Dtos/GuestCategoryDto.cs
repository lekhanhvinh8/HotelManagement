using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.Models.Dtos
{
    public class GuestCategoryDto
    {
        public GuestCategoryDto()
        {

        }

        public GuestCategoryDto(GuestCategory guestCategory)
        {
            this.Id = guestCategory.Id;
            this.Name = guestCategory.Name;
            this.Coefficient = guestCategory.Coefficient;
        }

        public GuestCategory CreateModel()
        {
            var guestCategory = new GuestCategory();

            guestCategory.Name = this.Name;
            guestCategory.Coefficient = this.Coefficient;

            return guestCategory;
        }

        public void Update(GuestCategory guestCategory)
        {
            guestCategory.Name = this.Name;
            guestCategory.Coefficient = this.Coefficient;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public float Coefficient { get; set; }
    }
}