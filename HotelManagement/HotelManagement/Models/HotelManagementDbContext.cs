using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HotelManagement.Models
{
    public class HotelManagementDbContext : DbContext
    {
        public DbSet<RoomCategory> RoomCategories { get; set; }
        public DbSet<Room> Rooms { get; set; }

        public DbSet<GuestCategory> GuestCategories { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<RoomRentalSlip> RoomRentalSlips { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public HotelManagementDbContext()
            :base("name=DefaultConnection")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Invoice>().Property(i => i.InvoiceID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<Invoice>().HasKey(i => i.InvoiceID);

            modelBuilder.Entity<Account>().Property(a => a.Username).HasMaxLength(449);
            modelBuilder.Entity<Account>().HasIndex(a => a.Username).IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}