namespace M19G1.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Booking_CheckinCheckout : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookings", "CheckInTime", c => c.DateTime());
            AddColumn("dbo.Bookings", "CheckOutTime", c => c.DateTime());
            DropColumn("dbo.Bookings", "CheckIn");
            DropColumn("dbo.Bookings", "CheckOut");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bookings", "CheckOut", c => c.Boolean(nullable: false));
            AddColumn("dbo.Bookings", "CheckIn", c => c.Boolean(nullable: false));
            DropColumn("dbo.Bookings", "CheckOutTime");
            DropColumn("dbo.Bookings", "CheckInTime");
        }
    }
}
