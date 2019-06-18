namespace M19G1.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIsUserLogedField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "IsUserLoged", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "IsUserLoged");
        }
    }
}
