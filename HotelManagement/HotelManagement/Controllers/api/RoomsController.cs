using HotelManagement.Models;
using HotelManagement.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HotelManagement.Controllers.api
{
    public class RoomsController : ApiController
    {
        HotelManagementDbContext _context;
        public RoomsController()
        {
            this._context = new HotelManagementDbContext();
        }

        public List<RoomDto> GetAll()
        {
            var roomsDto = new List<RoomDto>();

            foreach (var room in this._context.Rooms.ToList())
            {
                roomsDto.Add(new RoomDto(room));
            }

            return roomsDto;
        }
        public List<RoomDto> GetSome(int roomCategoryId)
        {
            var rooms = this._context.Rooms.Where(r => r.RoomCategoryId == roomCategoryId).ToList();

            var roomDtos = new List<RoomDto>();

            foreach (var room in rooms)
            {
                roomDtos.Add(new RoomDto(room));
            }

            return roomDtos;
        }
        public List<RoomDto> GetAvailableRooms()
        {
            var rooms = this._context.Rooms.Where(r => r.IsAvailable == true).ToList();

            var roomDtos = new List<RoomDto>();

            foreach (var room in rooms)
            {
                roomDtos.Add(new RoomDto(room));
            }

            return roomDtos;
        }

        public List<RoomDto> GetAvailableRooms(int roomCategoryId)
        {
            var rooms = this._context.Rooms.Where(r => r.IsAvailable == true)
                                           .Where(r => r.RoomCategoryId == roomCategoryId)
                                           .ToList();

            var roomDtos = new List<RoomDto>();

            foreach (var room in rooms)
            {
                roomDtos.Add(new RoomDto(room));
            }

            return roomDtos;
        }
        public RoomDto GetRoom(int id)
        {
            var room = this._context.Rooms.Find(id);
            if (room == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return new RoomDto(room);
        }

        [HttpPost]
        public RoomDto Create(RoomDto roomDto)
        {
            var room = roomDto.CreateModel();

            var roomCategory = this._context.RoomCategories.Find(roomDto.RoomCategoryID);
            room.RoomCategory = roomCategory;

            this._context.Rooms.Add(room);
            this._context.SaveChanges();

            return new RoomDto(room);
        }

        [HttpPut]
        public RoomDto Update(RoomDto roomDto)
        {
            var room = this._context.Rooms.Find(roomDto.Id);
            if (room == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            roomDto.Update(room);

            this._context.SaveChanges();

            return new RoomDto(room);
        }

        [HttpDelete]
        public RoomDto Delete(int id)
        {
            var room = this._context.Rooms.Find(id);
            if (room == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var roomDto = new RoomDto(room);

            this._context.Rooms.Remove(room);
            this._context.SaveChanges();

            return roomDto;
        }
    }
}
