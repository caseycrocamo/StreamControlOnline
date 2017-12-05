namespace StreamControl.Data
{
    using StreamControl.Models;
    using System;
    using System.Linq;
    using System.Data.Entity;

    public class ScoreBoardContext : DbContext
    {
        public ScoreBoardContext() : base("ScoreBoardContext")
        {
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Overlay> Scoreboards { get; set; }
        public DbSet<Style> Styles { get; set; }
        public DbSet<View> Views { get; set; }
        public DbSet<Element> Fields { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Style>().ToTable("Style");
            modelBuilder.Entity<Player>().ToTable("Player");
            modelBuilder.Entity<View>().ToTable("View");
            modelBuilder.Entity<Element>().ToTable("Field");
            modelBuilder.Entity<Overlay>().ToTable("Scoreboard");

        }
    }
}