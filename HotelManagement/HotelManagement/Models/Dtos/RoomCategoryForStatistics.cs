using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.Models.Dtos
{
    public class RoomCategoryForStatistics
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public float TotalCostInAMonth { get; set; }
    }
}