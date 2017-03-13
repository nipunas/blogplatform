namespace BlogOperations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BlogUserId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Blogs", "UserId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Blogs", "UserId");
        }
    }
}
