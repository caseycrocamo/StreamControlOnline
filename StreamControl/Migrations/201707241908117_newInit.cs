namespace StreamControl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newInit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Round",
                c => new
                    {
                        RoundId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.RoundId);
            
            AddColumn("dbo.Scoreboard", "Round_RoundId", c => c.Int());
            AddColumn("dbo.Style", "Player_PlayerID", c => c.Int());
            AddColumn("dbo.Style", "Round_RoundId", c => c.Int());
            CreateIndex("dbo.Style", "Player_PlayerID");
            CreateIndex("dbo.Style", "Round_RoundId");
            CreateIndex("dbo.Scoreboard", "Round_RoundId");
            AddForeignKey("dbo.Style", "Player_PlayerID", "dbo.Player", "PlayerID");
            AddForeignKey("dbo.Style", "Round_RoundId", "dbo.Round", "RoundId");
            AddForeignKey("dbo.Scoreboard", "Round_RoundId", "dbo.Round", "RoundId");
            DropColumn("dbo.Scoreboard", "Round");
            DropColumn("dbo.Style", "ScoreboardId");
            DropColumn("dbo.Style", "DivName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Style", "DivName", c => c.String());
            AddColumn("dbo.Style", "ScoreboardId", c => c.Int(nullable: false));
            AddColumn("dbo.Scoreboard", "Round", c => c.String());
            DropForeignKey("dbo.Scoreboard", "Round_RoundId", "dbo.Round");
            DropForeignKey("dbo.Style", "Round_RoundId", "dbo.Round");
            DropForeignKey("dbo.Style", "Player_PlayerID", "dbo.Player");
            DropIndex("dbo.Scoreboard", new[] { "Round_RoundId" });
            DropIndex("dbo.Style", new[] { "Round_RoundId" });
            DropIndex("dbo.Style", new[] { "Player_PlayerID" });
            DropColumn("dbo.Style", "Round_RoundId");
            DropColumn("dbo.Style", "Player_PlayerID");
            DropColumn("dbo.Scoreboard", "Round_RoundId");
            DropTable("dbo.Round");
        }
    }
}
