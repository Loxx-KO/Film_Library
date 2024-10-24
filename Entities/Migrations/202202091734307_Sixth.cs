namespace Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sixth : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CategoryMovies", newName: "MovieCategories");
            DropPrimaryKey("dbo.MovieCategories");
            AddPrimaryKey("dbo.MovieCategories", new[] { "Movie_Id", "Category_Id" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.MovieCategories");
            AddPrimaryKey("dbo.MovieCategories", new[] { "Category_Id", "Movie_Id" });
            RenameTable(name: "dbo.MovieCategories", newName: "CategoryMovies");
        }
    }
}
