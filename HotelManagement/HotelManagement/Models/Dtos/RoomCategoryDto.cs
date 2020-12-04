using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.Models.Dtos
{
    public class RoomCategoryDto
    {
        public RoomCategoryDto(Room_categories roomCategory)
        {
            this.id = roomCategory.id;
            this.name = roomCategory.name;
            this.unit_price = roomCategory.unit_price;
            this.max_number_of_guests = roomCategory.max_number_of_guests;
            this.surcharge_rate = roomCategory.surcharge_rate;
            this.num_start_surcharge = roomCategory.num_start_surcharge;
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
        public Nullable<float> unit_price { get; set; }
        public Nullable<int> max_number_of_guests { get; set; }
        public Nullable<int> num_start_surcharge { get; set; }
        public Nullable<float> surcharge_rate { get; set; }

        public List<RoomDto> RoomsDto;
        public Room_categories CreateModel()
        {
            var roomCategory = new Room_categories();
            roomCategory.name = this.name;
            roomCategory.max_number_of_guests = this.max_number_of_guests;
            roomCategory.num_start_surcharge = this.num_start_surcharge;
            roomCategory.surcharge_rate = this.surcharge_rate;
            roomCategory.unit_price = this.unit_price;

            return roomCategory;
        }

        public Room_categories Update(Room_categories roomCategory)
        {
            roomCategory.name = this.name;
            roomCategory.max_number_of_guests = this.max_number_of_guests;
            roomCategory.num_start_surcharge = this.num_start_surcharge;
            roomCategory.surcharge_rate = this.surcharge_rate;
            roomCategory.unit_price = this.unit_price;

            return roomCategory;
        }
    }
}