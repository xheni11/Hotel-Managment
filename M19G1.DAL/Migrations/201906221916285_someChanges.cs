namespace M19G1.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class someChanges : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnonymousRequests",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Confirmed = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Gender = c.String(maxLength: 1),
                        Birthday = c.DateTime(),
                        Enabled = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        IsUserLoged = c.Boolean(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Deleted = c.Boolean(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Deleted = c.Boolean(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 256),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        Birthday = c.DateTime(),
                        Gender = c.String(maxLength: 1),
                        RoleId = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookingTime = c.DateTime(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Valid = c.Boolean(nullable: false),
                        CheckInTime = c.DateTime(),
                        CheckOutTime = c.DateTime(),
                        UserId = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.BookingRooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoomId = c.Int(nullable: false),
                        BookingId = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .ForeignKey("dbo.Bookings", t => t.BookingId, cascadeDelete: true)
                .Index(t => t.RoomId)
                .Index(t => t.BookingId);
            
            CreateTable(
                "dbo.ExtraFacilities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BookingRoomId = c.Int(nullable: false),
                        FacilityId = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Facilities", t => t.FacilityId, cascadeDelete: true)
                .ForeignKey("dbo.BookingRooms", t => t.BookingRoomId, cascadeDelete: true)
                .Index(t => t.BookingRoomId)
                .Index(t => t.FacilityId);
            
            CreateTable(
                "dbo.Facilities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Available = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RoomFacilities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        RoomId = c.Int(nullable: false),
                        FacilityId = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .ForeignKey("dbo.Facilities", t => t.FacilityId, cascadeDelete: true)
                .Index(t => t.RoomId)
                .Index(t => t.FacilityId);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NrOfGuests = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Vacancy = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Occupied = c.Boolean(nullable: false),
                        Description = c.String(),
                        Enabled = c.Boolean(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RoomCategories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.RoomCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PersonalDriverServices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PickUpTime = c.DateTime(nullable: false),
                        Location = c.String(),
                        Destination = c.String(),
                        Completed = c.Boolean(nullable: false),
                        BookingId = c.Int(nullable: false),
                        DriverID = c.Int(nullable: false),
                        VoyageTime = c.Time(nullable: false, precision: 7),
                        Taken = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bookings", t => t.BookingId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.DriverID)
                .Index(t => t.BookingId)
                .Index(t => t.DriverID);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Rate = c.Double(nullable: false),
                        Description = c.String(),
                        DateCreated = c.DateTime(),
                        Deleted = c.Boolean(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bookings", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Log4NetLog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Thread = c.String(nullable: false, maxLength: 255, unicode: false),
                        Level = c.String(nullable: false, maxLength: 50, unicode: false),
                        Logger = c.String(nullable: false, maxLength: 255, unicode: false),
                        Message = c.String(nullable: false, maxLength: 4000, unicode: false),
                        Exception = c.String(maxLength: 2000, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        RoleId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PersonalDriverServices", "DriverID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bookings", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Ratings", "Id", "dbo.Bookings");
            DropForeignKey("dbo.PersonalDriverServices", "BookingId", "dbo.Bookings");
            DropForeignKey("dbo.BookingRooms", "BookingId", "dbo.Bookings");
            DropForeignKey("dbo.ExtraFacilities", "BookingRoomId", "dbo.BookingRooms");
            DropForeignKey("dbo.RoomFacilities", "FacilityId", "dbo.Facilities");
            DropForeignKey("dbo.RoomFacilities", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.Rooms", "CategoryId", "dbo.RoomCategories");
            DropForeignKey("dbo.BookingRooms", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.ExtraFacilities", "FacilityId", "dbo.Facilities");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserRequests", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AnonymousRequests", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.Ratings", new[] { "Id" });
            DropIndex("dbo.PersonalDriverServices", new[] { "DriverID" });
            DropIndex("dbo.PersonalDriverServices", new[] { "BookingId" });
            DropIndex("dbo.Rooms", new[] { "CategoryId" });
            DropIndex("dbo.RoomFacilities", new[] { "FacilityId" });
            DropIndex("dbo.RoomFacilities", new[] { "RoomId" });
            DropIndex("dbo.ExtraFacilities", new[] { "FacilityId" });
            DropIndex("dbo.ExtraFacilities", new[] { "BookingRoomId" });
            DropIndex("dbo.BookingRooms", new[] { "BookingId" });
            DropIndex("dbo.BookingRooms", new[] { "RoomId" });
            DropIndex("dbo.Bookings", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.UserRequests", new[] { "RoleId" });
            DropIndex("dbo.AnonymousRequests", new[] { "Id" });
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Log4NetLog");
            DropTable("dbo.Ratings");
            DropTable("dbo.PersonalDriverServices");
            DropTable("dbo.RoomCategories");
            DropTable("dbo.Rooms");
            DropTable("dbo.RoomFacilities");
            DropTable("dbo.Facilities");
            DropTable("dbo.ExtraFacilities");
            DropTable("dbo.BookingRooms");
            DropTable("dbo.Bookings");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.UserRequests");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AnonymousRequests");
        }
    }
}
