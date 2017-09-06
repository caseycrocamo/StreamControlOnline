namespace StreamControl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ViewUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Style", "Score_ScoreId", "dbo.Scores");
            DropForeignKey("dbo.Player", "Score_ScoreId", "dbo.Scores");
            DropForeignKey("dbo.Style", "Player_PlayerID", "dbo.Player");
            DropForeignKey("dbo.Style", "Round_RoundId", "dbo.Round");
            DropForeignKey("dbo.Scoreboard", "Player1_PlayerID", "dbo.Player");
            DropForeignKey("dbo.Scoreboard", "Player2_PlayerID", "dbo.Player");
            DropForeignKey("dbo.Scoreboard", "Round_RoundId", "dbo.Round");
            DropIndex("dbo.Player", new[] { "Score_ScoreId" });
            DropIndex("dbo.Style", new[] { "Score_ScoreId" });
            DropIndex("dbo.Style", new[] { "Player_PlayerID" });
            DropIndex("dbo.Style", new[] { "Round_RoundId" });
            DropIndex("dbo.Scoreboard", new[] { "Player1_PlayerID" });
            DropIndex("dbo.Scoreboard", new[] { "Player2_PlayerID" });
            DropIndex("dbo.Scoreboard", new[] { "Round_RoundId" });
            CreateTable(
                "dbo.Field",
                c => new
                    {
                        FieldID = c.Int(nullable: false, identity: true),
                        Label = c.String(),
                        Value = c.String(),
                        Scoreboard_ScoreboardID = c.Int(),
                    })
                .PrimaryKey(t => t.FieldID)
                .ForeignKey("dbo.Scoreboard", t => t.Scoreboard_ScoreboardID)
                .Index(t => t.Scoreboard_ScoreboardID);
            
            CreateTable(
                "dbo.View",
                c => new
                    {
                        ViewID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Scoreboard_ScoreboardID = c.Int(),
                    })
                .PrimaryKey(t => t.ViewID)
                .ForeignKey("dbo.Scoreboard", t => t.Scoreboard_ScoreboardID)
                .Index(t => t.Scoreboard_ScoreboardID);
            
            AddColumn("dbo.Player", "Label", c => c.String());
            AddColumn("dbo.Player", "Scoreboard_ScoreboardID", c => c.Int());
            AddColumn("dbo.Style", "DivID", c => c.String());
            AddColumn("dbo.Style", "Display", c => c.String());
            AddColumn("dbo.Style", "Position", c => c.String());
            AddColumn("dbo.Style", "Left", c => c.String());
            AddColumn("dbo.Style", "Top", c => c.String());
            AddColumn("dbo.Style", "Right", c => c.String());
            AddColumn("dbo.Style", "Bottom", c => c.String());
            AddColumn("dbo.Style", "TextAlign", c => c.String());
            AddColumn("dbo.Style", "Color", c => c.String());
            AddColumn("dbo.Style", "BackgroundColor", c => c.String());
            AddColumn("dbo.Style", "Font", c => c.String());
            AddColumn("dbo.Style", "Width", c => c.String());
            AddColumn("dbo.Style", "FontSize", c => c.String());
            AddColumn("dbo.Style", "Height", c => c.String());
            AddColumn("dbo.Style", "Padding", c => c.String());
            AddColumn("dbo.Style", "View_ViewID", c => c.Int());
            CreateIndex("dbo.Player", "Scoreboard_ScoreboardID");
            CreateIndex("dbo.Style", "View_ViewID");
            AddForeignKey("dbo.Player", "Scoreboard_ScoreboardID", "dbo.Scoreboard", "ScoreboardID");
            AddForeignKey("dbo.Style", "View_ViewID", "dbo.View", "ViewID");
            DropColumn("dbo.Player", "OwnerID");
            DropColumn("dbo.Player", "Score_ScoreId");
            DropColumn("dbo.Style", "AttributeName");
            DropColumn("dbo.Style", "Value");
            DropColumn("dbo.Style", "Score_ScoreId");
            DropColumn("dbo.Style", "Player_PlayerID");
            DropColumn("dbo.Style", "Round_RoundId");
            DropColumn("dbo.Scoreboard", "Player1_PlayerID");
            DropColumn("dbo.Scoreboard", "Player2_PlayerID");
            DropColumn("dbo.Scoreboard", "Round_RoundId");
            DropTable("dbo.Scores");
            DropTable("dbo.Round");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Round",
                c => new
                    {
                        RoundId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.RoundId);
            
            CreateTable(
                "dbo.Scores",
                c => new
                    {
                        ScoreId = c.Int(nullable: false, identity: true),
                        Value = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ScoreId);
            
            AddColumn("dbo.Scoreboard", "Round_RoundId", c => c.Int());
            AddColumn("dbo.Scoreboard", "Player2_PlayerID", c => c.Int());
            AddColumn("dbo.Scoreboard", "Player1_PlayerID", c => c.Int());
            AddColumn("dbo.Style", "Round_RoundId", c => c.Int());
            AddColumn("dbo.Style", "Player_PlayerID", c => c.Int());
            AddColumn("dbo.Style", "Score_ScoreId", c => c.Int());
            AddColumn("dbo.Style", "Value", c => c.String());
            AddColumn("dbo.Style", "AttributeName", c => c.String());
            AddColumn("dbo.Player", "Score_ScoreId", c => c.Int());
            AddColumn("dbo.Player", "OwnerID", c => c.String());
            DropForeignKey("dbo.View", "Scoreboard_ScoreboardID", "dbo.Scoreboard");
            DropForeignKey("dbo.Style", "View_ViewID", "dbo.View");
            DropForeignKey("dbo.Player", "Scoreboard_ScoreboardID", "dbo.Scoreboard");
            DropForeignKey("dbo.Field", "Scoreboard_ScoreboardID", "dbo.Scoreboard");
            DropIndex("dbo.Style", new[] { "View_ViewID" });
            DropIndex("dbo.View", new[] { "Scoreboard_ScoreboardID" });
            DropIndex("dbo.Player", new[] { "Scoreboard_ScoreboardID" });
            DropIndex("dbo.Field", new[] { "Scoreboard_ScoreboardID" });
            DropColumn("dbo.Style", "View_ViewID");
            DropColumn("dbo.Style", "Padding");
            DropColumn("dbo.Style", "Height");
            DropColumn("dbo.Style", "FontSize");
            DropColumn("dbo.Style", "Width");
            DropColumn("dbo.Style", "Font");
            DropColumn("dbo.Style", "BackgroundColor");
            DropColumn("dbo.Style", "Color");
            DropColumn("dbo.Style", "TextAlign");
            DropColumn("dbo.Style", "Bottom");
            DropColumn("dbo.Style", "Right");
            DropColumn("dbo.Style", "Top");
            DropColumn("dbo.Style", "Left");
            DropColumn("dbo.Style", "Position");
            DropColumn("dbo.Style", "Display");
            DropColumn("dbo.Style", "DivID");
            DropColumn("dbo.Player", "Scoreboard_ScoreboardID");
            DropColumn("dbo.Player", "Label");
            DropTable("dbo.View");
            DropTable("dbo.Field");
            CreateIndex("dbo.Scoreboard", "Round_RoundId");
            CreateIndex("dbo.Scoreboard", "Player2_PlayerID");
            CreateIndex("dbo.Scoreboard", "Player1_PlayerID");
            CreateIndex("dbo.Style", "Round_RoundId");
            CreateIndex("dbo.Style", "Player_PlayerID");
            CreateIndex("dbo.Style", "Score_ScoreId");
            CreateIndex("dbo.Player", "Score_ScoreId");
            AddForeignKey("dbo.Scoreboard", "Round_RoundId", "dbo.Round", "RoundId");
            AddForeignKey("dbo.Scoreboard", "Player2_PlayerID", "dbo.Player", "PlayerID");
            AddForeignKey("dbo.Scoreboard", "Player1_PlayerID", "dbo.Player", "PlayerID");
            AddForeignKey("dbo.Style", "Round_RoundId", "dbo.Round", "RoundId");
            AddForeignKey("dbo.Style", "Player_PlayerID", "dbo.Player", "PlayerID");
            AddForeignKey("dbo.Player", "Score_ScoreId", "dbo.Scores", "ScoreId");
            AddForeignKey("dbo.Style", "Score_ScoreId", "dbo.Scores", "ScoreId");
        }
    }
}
