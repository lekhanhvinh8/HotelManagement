using HotelManagement.Models;
using HotelManagement.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HotelManagement.Controllers.api
{
    [System.Runtime.InteropServices.Guid("B57D4C5A-2587-49E1-876C-759CEBBFB573")]
    public class StatisticController : ApiController
    {
        private HotelManagementDbContext _context;
        public StatisticController()
        {
            this._context = new HotelManagementDbContext();
        }

        [HttpGet]
        public List<RoomCategoryForStatistics> GetAllRoomCategories(int month, int year)
        {
            var invoices = this._context.Invoices.Where(i => (i.DateOfInvoice.Year == year) &&
                                                             (i.DateOfInvoice.Month == month)).ToList();

            var roomCategoriesForStatistics = new List<RoomCategoryForStatistics>();

            foreach (var roomCategory in this._context.RoomCategories.ToList())
            {
                var roomCategoryForStatistics = new RoomCategoryForStatistics();

                roomCategoryForStatistics.ID = roomCategory.Id;
                roomCategoryForStatistics.Name = roomCategory.Name;
                roomCategoryForStatistics.TotalCostInAMonth = 0;

                roomCategoriesForStatistics.Add(roomCategoryForStatistics);
            }

            foreach (var invoice in invoices)
            {
                foreach (var roomRentalSlip in invoice.RoomRentalSlips)
                {
                    var roomRentalSlipFullInfoDto = new RoomRentalSlipFullInfoDto(roomRentalSlip);

                    var roomCategory = roomCategoriesForStatistics.Find(r => r.ID == roomRentalSlip.Room.RoomCategory.Id);

                    if (roomCategory == null)
                        throw new Exception("Not found");

                    roomCategory.TotalCostInAMonth += roomRentalSlipFullInfoDto.TotalCost;

                }
            }

            return roomCategoriesForStatistics;
        }

        [HttpGet]
        public List<RoomForStatistics> GetAllRooms(int month, int year, int roomCategoryId)
        {
            var invoices = this._context.Invoices.Where(i => (i.DateOfInvoice.Year == year) &&
                                                                 (i.DateOfInvoice.Month == month)).ToList();

            var roomsForStatistics = new List<RoomForStatistics>();

            var roomCategory = this._context.RoomCategories.Find(roomCategoryId);
            if (roomCategory == null)
                throw new Exception("Not found");

            foreach (var room in roomCategory.Rooms)
            {
                var roomForStatistics = new RoomForStatistics();

                roomForStatistics.roomID = room.Id;
                roomForStatistics.roomNumber = room.RoomNumber;
                roomForStatistics.numberRentalDays = 0;

                roomsForStatistics.Add(roomForStatistics);
            }

            foreach (var invoice in invoices)
            {
                foreach (var roomRentalSlip in invoice.RoomRentalSlips)
                {
                    var roomForStatistics = roomsForStatistics.Find(r => r.roomID == roomRentalSlip.RoomId);

                    if (roomForStatistics == null)
                        continue;

                    var days = new RoomRentalSlipFullInfoDto(roomRentalSlip).Days;

                    roomForStatistics.numberRentalDays += days;
                }
            }

            return roomsForStatistics;
        }
    }
}
