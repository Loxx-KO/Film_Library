namespace Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ninth : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Movies", "CategoryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "CategoryId", c => c.Int(nullable: false));
        }
    }
}
