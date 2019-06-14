namespace M19G1.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fkAnonymous : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AnonymousRequests", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AnonymousRequests", "UserId", c => c.Int(nullable: false));
        }
    }
}
