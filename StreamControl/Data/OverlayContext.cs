namespace StreamControl.Data
{
    using StreamControl.Models;
    using System;
    using System.Linq;
    using System.Data.Entity;

    public class OverlayContext : DbContext
    {
        public OverlayContext() : base("OverlayContext")
        {
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Overlay> Overlays { get; set; }
        public DbSet<Style> Styles { get; set; }
        public DbSet<View> Views { get; set; }
        public DbSet<PlayerElement> PlayerElements { get; set; }
        public DbSet<TextElement> TextElements { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Style>().ToTable("Style");
            modelBuilder.Entity<Player>().ToTable("Player");
            modelBuilder.Entity<View>().ToTable("View");
            modelBuilder.Entity<PlayerElement>().ToTable("PlayerElement");
            modelBuilder.Entity<TextElement>().ToTable("TextElement");
            modelBuilder.Entity<Overlay>().ToTable("Overlay");
        }
    }
}