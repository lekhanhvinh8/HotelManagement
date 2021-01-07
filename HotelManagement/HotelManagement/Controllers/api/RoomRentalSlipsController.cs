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
    public class RoomRentalSlipsController : ApiController
    {
        private HotelManagementDbContext _context;

        public RoomRentalSlipsController()
        {
            this._context = new HotelManagementDbContext();
        }

        [HttpGet]
        public List<RoomRentalSlipDto> GetAll()
        {
            var roomRentalSlipDtos = new List<RoomRentalSlipDto>();

            foreach (var roomRentalSlip in this._context.RoomRentalSlips.ToList())
            {
                roomRentalSlipDtos.Add(new RoomRentalSlipDto(roomRentalSlip));
            }

            return roomRentalSlipDtos;
        }

        [HttpGet]
        public RoomRentalSlipDto Get(int id)
        {
            var roomRentalSlip = this._context.RoomRentalSlips.Find(id);

            if (roomRentalSlip == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return new RoomRentalSlipDto(roomRentalSlip);
        }

        [HttpPost]
        public RoomRentalSlipDto Create(RoomRentalSlipDto roomRentalSlipDto)
        {
            var roomRentalSlip = roomRentalSlipDto.CreateModel();
            var room = this._context.Rooms.Find(roomRentalSlip.RoomId);

            if (room == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            if (room.IsAvailable == false)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            foreach (var guest in roomRentalSlip.Guests)
            {
                guest.GuestCategory = this._context.GuestCategories.Find(guest.GuestCategoryId);
            }

            room.IsAvailable = false;

            this._context.RoomRentalSlips.Add(roomRentalSlip);
            this._context.SaveChanges();

            return new RoomRentalSlipDto(roomRentalSlip);
        }
    }
}
