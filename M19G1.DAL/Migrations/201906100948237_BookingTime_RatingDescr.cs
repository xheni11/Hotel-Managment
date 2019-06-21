namespace M19G1.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BookingTime_RatingDescr : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookings", "BookingTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Ratings", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ratings", "Description");
            DropColumn("dbo.Bookings", "BookingTime");
        }
    }
}
