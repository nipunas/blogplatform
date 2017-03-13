namespace BlogOperations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class postimagename : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "ImageName", c => c.String());
            DropColumn("dbo.Posts", "ImageId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "ImageId", c => c.Guid(nullable: false));
            DropColumn("dbo.Posts", "ImageName");
        }
    }
}
