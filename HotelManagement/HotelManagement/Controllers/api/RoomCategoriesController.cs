using HotelManagement.Models;
using HotelManagement.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
namespace HotelManagement.Controllers.api
{
    public class RoomCategoriesController : ApiController
    {
        private HotelManagementDbContext _context;

        public RoomCategoriesController()
        {
            this._context = new HotelManagementDbContext();
        }

        public List<RoomCategoryDto> GetAll()
        {
            var roomCategoriesDto = new List<RoomCategoryDto>();
            foreach (var roomCategory in this._context.Room_categories.Include(r => r.Rooms).ToList())
            {
                roomCategoriesDto.Add(new RoomCategoryDto(roomCategory));
            }

            return roomCategoriesDto;
        }

        public RoomCategoryDto Get(int id)
        {
            var roomCategory = this._context.Room_categories.Include(r => r.Rooms).SingleOrDefault(r => r.id == id);

            if (roomCategory == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return new RoomCategoryDto(roomCategory);
        }

        [HttpPost]
        public RoomCategoryDto CreateNew(RoomCategoryDto roomCategoryDto)
        {
            var roomCategory = roomCategoryDto.CreateModel();

            this._context.Room_categories.Add(roomCategory);
            this._context.SaveChanges();

            return new RoomCategoryDto(roomCategory);
        }

        [HttpPut]
        public RoomCategoryDto Update(RoomCategoryDto roomCategoryDto) 
        {
            var roomCategory = this._context.Room_categories.Include(r => r.Rooms).SingleOrDefault(r => r.id == roomCategoryDto.id);

            if (roomCategory == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            roomCategoryDto.Update(roomCategory);
            this._context.SaveChanges();

            return new RoomCategoryDto(roomCategory);
        }

        [HttpDelete]
        public RoomCategoryDto Delete(int id)
        {
            var roomCategory = this._context.Room_categories.Find(id);

            if (roomCategory == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            this._context.Room_categories.Remove(roomCategory);
            this._context.SaveChanges();

            return new RoomCategoryDto(roomCategory);
        }
    }
}
