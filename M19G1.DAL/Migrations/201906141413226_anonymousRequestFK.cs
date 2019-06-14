namespace M19G1.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class anonymousRequestFK : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AnonymousRequests", "UserId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AnonymousRequests", "UserId");
        }
    }
}
