namespace M19G1.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteSomeFieldsUserRequests : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserRequests", "EmailConfirmed");
            DropColumn("dbo.UserRequests", "DateCreated");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserRequests", "DateCreated", c => c.DateTime());
            AddColumn("dbo.UserRequests", "EmailConfirmed", c => c.Boolean(nullable: false));
        }
    }
}
