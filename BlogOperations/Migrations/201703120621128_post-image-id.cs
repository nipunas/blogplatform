namespace BlogOperations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class postimageid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "ImageId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "ImageId");
        }
    }
}
