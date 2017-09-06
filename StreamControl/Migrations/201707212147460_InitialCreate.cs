namespace StreamControl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CssAttribute",
                c => new
                    {
                        CssAttributeID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CssAttributeID);
            
            CreateTable(
                "dbo.Player",
                c => new
                    {
                        PlayerID = c.Int(nullable: false, identity: true),
                        OwnerID = c.String(),
                        Name = c.String(),
                        Score = c.Int(nullable: false),
                        Character = c.String(),
                    })
                .PrimaryKey(t => t.PlayerID);
            
            CreateTable(
                "dbo.Scoreboard",
                c => new
                    {
                        ScoreboardID = c.Int(nullable: false, identity: true),
                        OwnerID = c.String(),
                        Name = c.String(),
                        Round = c.String(),
                        Player1_PlayerID = c.Int(),
                        Player2_PlayerID = c.Int(),
                    })
                .PrimaryKey(t => t.ScoreboardID)
                .ForeignKey("dbo.Player", t => t.Player1_PlayerID)
                .ForeignKey("dbo.Player", t => t.Player2_PlayerID)
                .Index(t => t.Player1_PlayerID)
                .Index(t => t.Player2_PlayerID);
            
            CreateTable(
                "dbo.Style",
                c => new
                    {
                        StyleID = c.Int(nullable: false, identity: true),
                        ScoreboardId = c.Int(nullable: false),
                        DivName = c.String(),
                        AttributeId = c.Int(nullable: false),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.StyleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Scoreboard", "Player2_PlayerID", "dbo.Player");
            DropForeignKey("dbo.Scoreboard", "Player1_PlayerID", "dbo.Player");
            DropIndex("dbo.Scoreboard", new[] { "Player2_PlayerID" });
            DropIndex("dbo.Scoreboard", new[] { "Player1_PlayerID" });
            DropTable("dbo.Style");
            DropTable("dbo.Scoreboard");
            DropTable("dbo.Player");
            DropTable("dbo.CssAttribute");
        }
    }
}
