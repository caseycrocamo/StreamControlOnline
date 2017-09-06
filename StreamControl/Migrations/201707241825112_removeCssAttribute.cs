namespace StreamControl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeCssAttribute : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Style", "AttributeName", c => c.String());
            DropColumn("dbo.Style", "AttributeId");
            DropTable("dbo.CssAttribute");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CssAttribute",
                c => new
                    {
                        CssAttributeID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CssAttributeID);
            
            AddColumn("dbo.Style", "AttributeId", c => c.Int(nullable: false));
            DropColumn("dbo.Style", "AttributeName");
        }
    }
}
