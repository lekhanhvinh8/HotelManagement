﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HotelManagement.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class HotelManagementDbContext : DbContext
    {
        public HotelManagementDbContext()
            : base("name=HotelManagementDbContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<Guest_categories> Guest_categories { get; set; }
        public virtual DbSet<Guest> Guests { get; set; }
        public virtual DbSet<Room_categories> Room_categories { get; set; }
        public virtual DbSet<Room_rental_slip> Room_rental_slip { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
    }
}