using M19G1.DAL.Entities;
using System.Data.Entity;

namespace M19G1.DAL.Persistence
{
    public partial class M19G1Context : DbContext
    {
        public M19G1Context()
            : base("name=M19G1Context")
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Log4NetLog> Log4NetLog { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<RoomCategory> RoomCategories { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<BookingRoom> BookingRooms { get; set; }
        public virtual DbSet<Facility> Facilities { get; set; }
        public virtual DbSet<PersonalDriverService> DriverServices { get; set; }
        public virtual DbSet<RoomFacility> RoomFacilities { get; set; }
        public virtual DbSet<ExtraFacility> ExtraFacilities { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<UserRequest> UserRequests { get; set; }
        public virtual DbSet<AnonymousRequest> AnonymousRequests { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Log4NetLog>()
                .Property(e => e.Thread)
                .IsUnicode(false);

            modelBuilder.Entity<Log4NetLog>()
                .Property(e => e.Level)
                .IsUnicode(false);

            modelBuilder.Entity<Log4NetLog>()
                .Property(e => e.Logger)
                .IsUnicode(false);

            modelBuilder.Entity<Log4NetLog>()
                .Property(e => e.Message)
                .IsUnicode(false);

            modelBuilder.Entity<Log4NetLog>()
                .Property(e => e.Exception)
                .IsUnicode(false);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Bookings)
                .WithRequired(b => b.Client)
                .HasForeignKey(b => b.UserId);

            modelBuilder.Entity<Booking>()
                .HasMany(b => b.DriverServices)
                .WithRequired(d => d.Booking)
                .HasForeignKey(d => d.BookingId);

            modelBuilder.Entity<Booking>()
                .HasMany(b => b.BookingRooms)
                .WithRequired(br => br.Booking)
                .HasForeignKey(br => br.BookingId);

            modelBuilder.Entity<RoomCategory>()
                .HasMany(c => c.Rooms)
                .WithRequired(r => r.Category)
                .HasForeignKey(r => r.CategoryId);

            modelBuilder.Entity<Room>()
                .HasMany(r => r.BookingRooms)
                .WithRequired(br => br.Room)
                .HasForeignKey(br => br.RoomId);

            modelBuilder.Entity<Facility>()
                .HasMany(f => f.RoomFacilities)
                .WithRequired(rf => rf.Facility)
                .HasForeignKey(rf => rf.FacilityId);

            modelBuilder.Entity<Facility>()
                .HasMany(f => f.ExtraFacilities)
                .WithRequired(ef => ef.Facility)
                .HasForeignKey(ef => ef.FacilityId);

            modelBuilder.Entity<Room>()
                .HasMany(r => r.RoomFacilities)
                .WithRequired(rf => rf.Room)
                .HasForeignKey(rf => rf.RoomId);

            modelBuilder.Entity<BookingRoom>()
                .HasMany(br => br.ExtraFacilities)
                .WithRequired(ef => ef.BookingRoom)
                .HasForeignKey(ef => ef.BookingRoomId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(u => u.DriverServices)
                .WithRequired(ds => ds.Driver)
                .HasForeignKey(ds => ds.DriverId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetRole>()
                .HasMany(r => r.UserRequests)
                .WithRequired(ur => ur.Role)
                .HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<AspNetUser>()
                .HasOptional(u => u.AnonymousRequest)
                .WithRequired(ur => ur.User);

            modelBuilder.Entity<Booking>()
                .HasOptional(b => b.Rating)
                .WithRequired(r => r.Booking);
            modelBuilder.Entity<AnonymousRequest>()
                .HasKey(ar => ar.Id);






        }
    }
}
