namespace Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        DirectorId = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Directors", t => t.DirectorId, cascadeDelete: true)
                .Index(t => t.DirectorId);
            
            CreateTable(
                "dbo.Directors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Surname = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 60),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderMovies",
                c => new
                    {
                        Order_Id = c.Int(nullable: false),
                        Movie_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Order_Id, t.Movie_Id })
                .ForeignKey("dbo.Orders", t => t.Order_Id, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.Movie_Id, cascadeDelete: true)
                .Index(t => t.Order_Id)
                .Index(t => t.Movie_Id);
            
            CreateTable(
                "dbo.CategoryMovies",
                c => new
                    {
                        Category_Id = c.Int(nullable: false),
                        Movie_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_Id, t.Movie_Id })
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.Movie_Id, cascadeDelete: true)
                .Index(t => t.Category_Id)
                .Index(t => t.Movie_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CategoryMovies", "Movie_Id", "dbo.Movies");
            DropForeignKey("dbo.CategoryMovies", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.OrderMovies", "Movie_Id", "dbo.Movies");
            DropForeignKey("dbo.OrderMovies", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.Movies", "DirectorId", "dbo.Directors");
            DropIndex("dbo.CategoryMovies", new[] { "Movie_Id" });
            DropIndex("dbo.CategoryMovies", new[] { "Category_Id" });
            DropIndex("dbo.OrderMovies", new[] { "Movie_Id" });
            DropIndex("dbo.OrderMovies", new[] { "Order_Id" });
            DropIndex("dbo.Movies", new[] { "DirectorId" });
            DropTable("dbo.CategoryMovies");
            DropTable("dbo.OrderMovies");
            DropTable("dbo.Orders");
            DropTable("dbo.Directors");
            DropTable("dbo.Movies");
            DropTable("dbo.Categories");
        }
    }
}
