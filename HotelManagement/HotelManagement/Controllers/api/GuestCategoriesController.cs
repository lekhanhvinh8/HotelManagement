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
    public class GuestCategoriesController : ApiController
    {
        private HotelManagementDbContext _context;

        public GuestCategoriesController()
        {
            this._context = new HotelManagementDbContext();
        }

        [HttpGet]
        public List<GuestCategoryDto> GetAll()
        {
            var guestCategoryDtos = new List<GuestCategoryDto>();

            foreach (var guestCategory in this._context.GuestCategories.ToList())
            {
                guestCategoryDtos.Add(new GuestCategoryDto(guestCategory));
            }

            return guestCategoryDtos;
        }

        [HttpGet]
        public GuestCategoryDto Get(int id)
        {
            var guestCategory = this._context.GuestCategories.Find(id);

            if (guestCategory == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return new GuestCategoryDto(guestCategory);
        }

        [HttpPost]
        public GuestCategoryDto Create(GuestCategoryDto guestCategoryDto)
        {
            var guestCategory = guestCategoryDto.CreateModel();

            this._context.GuestCategories.Add(guestCategory);
            this._context.SaveChanges();

            return new GuestCategoryDto(guestCategory);
        }

        [HttpPut]
        public GuestCategoryDto Update(GuestCategoryDto guestCategoryDto)
        {
            var guestCategory = this._context.GuestCategories.Find(guestCategoryDto.Id);

            if (guestCategory == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            guestCategoryDto.Update(guestCategory);
            this._context.SaveChanges();

            return new GuestCategoryDto(guestCategory);
        }

        [HttpDelete]
        public void Delete(int id)
        {
            var guestCategory = this._context.GuestCategories.Find(id);

            if (guestCategory == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            this._context.GuestCategories.Remove(guestCategory);
            this._context.SaveChanges();
        }

    }
}
