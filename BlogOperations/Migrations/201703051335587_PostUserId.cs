namespace BlogOperations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostUserId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "UserId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "UserId");
        }
    }
}
