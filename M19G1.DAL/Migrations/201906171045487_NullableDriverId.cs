namespace M19G1.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableDriverId : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.PersonalDriverServices", new[] { "DriverId" });
            CreateIndex("dbo.PersonalDriverServices", "DriverID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.PersonalDriverServices", new[] { "DriverID" });
            CreateIndex("dbo.PersonalDriverServices", "DriverId");
        }
    }
}
