using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.Models.Dtos
{
    public class RoomCategoryDto
    {
        public RoomCategoryDto(RoomCategory roomCategory)
        {
            this.id = roomCategory.Id;
            this.name = roomCategory.Name;
            this.UnitPrice = roomCategory.UnitPrice;
            this.MaxNumberOfGuest = roomCategory.MaxNumberOfGuests;
            this.SurchargeRate = roomCategory.SurchargeRate;
            this.NumStartSurcharge = roomCategory.NumStartSurcharge;
            this.RoomsDto = new List<RoomDto>();
            foreach (var room in roomCategory.Rooms)
            {
                this.RoomsDto.Add(new RoomDto(room));
            }
        }
        public RoomCategoryDto()
        {
            this.RoomsDto = new List<RoomDto>();
        }

        public int id { get; set; }
        public string name { get; set; }
        public float UnitPrice { get; set; }
        public int MaxNumberOfGuest { get; set; }
        public int NumStartSurcharge { get; set; }
        public float SurchargeRate { get; set; }

        public List<RoomDto> RoomsDto;
        public RoomCategory CreateModel()
        {
            var roomCategory = new RoomCategory();
            roomCategory.Name = this.name;
            roomCategory.MaxNumberOfGuests = this.MaxNumberOfGuest;
            roomCategory.NumStartSurcharge = this.NumStartSurcharge;
            roomCategory.SurchargeRate = this.SurchargeRate;
            roomCategory.UnitPrice = this.UnitPrice;

            return roomCategory;
        }

        public RoomCategory Update(RoomCategory roomCategory)
        {
            roomCategory.Name = this.name;
            roomCategory.MaxNumberOfGuests = this.MaxNumberOfGuest;
            roomCategory.NumStartSurcharge = this.NumStartSurcharge;
            roomCategory.SurchargeRate = this.SurchargeRate;
            roomCategory.UnitPrice = this.UnitPrice;

            return roomCategory;
        }
    }
}