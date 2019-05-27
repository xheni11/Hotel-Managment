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
        }
    }
}
