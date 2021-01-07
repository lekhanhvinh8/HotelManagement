using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.Models.Dtos
{
    public class RoomForStatistics
    {
        public int roomID { get; set; }
        public string roomNumber { get; set; }
        public int numberRentalDays { get; set; }
    }
}