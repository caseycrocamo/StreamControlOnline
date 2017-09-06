namespace StreamControl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foreignkeyfix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Field", "Scoreboard_ScoreboardID", "dbo.Scoreboard");
            DropForeignKey("dbo.Player", "Scoreboard_ScoreboardID", "dbo.Scoreboard");
            DropForeignKey("dbo.View", "Scoreboard_ScoreboardID", "dbo.Scoreboard");
            DropIndex("dbo.Field", new[] { "Scoreboard_ScoreboardID" });
            DropIndex("dbo.Player", new[] { "Scoreboard_ScoreboardID" });
            DropIndex("dbo.View", new[] { "Scoreboard_ScoreboardID" });
            RenameColumn(table: "dbo.Field", name: "Scoreboard_ScoreboardID", newName: "ScoreboardID");
            RenameColumn(table: "dbo.Player", name: "Scoreboard_ScoreboardID", newName: "ScoreboardID");
            RenameColumn(table: "dbo.View", name: "Scoreboard_ScoreboardID", newName: "ScoreboardID");
            AlterColumn("dbo.Field", "ScoreboardID", c => c.Int(nullable: false));
            AlterColumn("dbo.Player", "ScoreboardID", c => c.Int(nullable: false));
            AlterColumn("dbo.View", "ScoreboardID", c => c.Int(nullable: false));
            CreateIndex("dbo.Field", "ScoreboardID");
            CreateIndex("dbo.Player", "ScoreboardID");
            CreateIndex("dbo.View", "ScoreboardID");
            AddForeignKey("dbo.Field", "ScoreboardID", "dbo.Scoreboard", "ScoreboardID", cascadeDelete: true);
            AddForeignKey("dbo.Player", "ScoreboardID", "dbo.Scoreboard", "ScoreboardID", cascadeDelete: true);
            AddForeignKey("dbo.View", "ScoreboardID", "dbo.Scoreboard", "ScoreboardID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.View", "ScoreboardID", "dbo.Scoreboard");
            DropForeignKey("dbo.Player", "ScoreboardID", "dbo.Scoreboard");
            DropForeignKey("dbo.Field", "ScoreboardID", "dbo.Scoreboard");
            DropIndex("dbo.View", new[] { "ScoreboardID" });
            DropIndex("dbo.Player", new[] { "ScoreboardID" });
            DropIndex("dbo.Field", new[] { "ScoreboardID" });
            AlterColumn("dbo.View", "ScoreboardID", c => c.Int());
            AlterColumn("dbo.Player", "ScoreboardID", c => c.Int());
            AlterColumn("dbo.Field", "ScoreboardID", c => c.Int());
            RenameColumn(table: "dbo.View", name: "ScoreboardID", newName: "Scoreboard_ScoreboardID");
            RenameColumn(table: "dbo.Player", name: "ScoreboardID", newName: "Scoreboard_ScoreboardID");
            RenameColumn(table: "dbo.Field", name: "ScoreboardID", newName: "Scoreboard_ScoreboardID");
            CreateIndex("dbo.View", "Scoreboard_ScoreboardID");
            CreateIndex("dbo.Player", "Scoreboard_ScoreboardID");
            CreateIndex("dbo.Field", "Scoreboard_ScoreboardID");
            AddForeignKey("dbo.View", "Scoreboard_ScoreboardID", "dbo.Scoreboard", "ScoreboardID");
            AddForeignKey("dbo.Player", "Scoreboard_ScoreboardID", "dbo.Scoreboard", "ScoreboardID");
            AddForeignKey("dbo.Field", "Scoreboard_ScoreboardID", "dbo.Scoreboard", "ScoreboardID");
        }
    }
}
