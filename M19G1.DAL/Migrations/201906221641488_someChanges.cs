namespace M19G1.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class someChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "Enabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ratings", "DateCreated", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ratings", "DateCreated");
            DropColumn("dbo.Rooms", "Enabled");
        }
    }
}
