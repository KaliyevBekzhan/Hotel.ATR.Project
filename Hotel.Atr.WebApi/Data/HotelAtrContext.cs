using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Hotel.Atr.WebApi.Models;

namespace Hotel.Atr.WebApi.Data
{
    public partial class HotelAtrContext : DbContext
    {
        public HotelAtrContext()
        {
        }

        public HotelAtrContext(DbContextOptions<HotelAtrContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<RoomDatum> RoomData { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("data source=178.89.186.221, 1434;initial catalog=kaliev_db;user id=kaliev_user;password=^e694y1Vu;MultipleActiveResultSets=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("kaliev_user");

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasIndex(e => e.RoomDataid, "IX_Clients_RoomDataid");

                entity.HasOne(d => d.RoomData)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.RoomDataid);
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasIndex(e => e.RoomDataid, "IX_Rooms_RoomDataid");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PathToImage).HasDefaultValueSql("(N'')");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.RoomData)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.RoomDataid);
            });

            modelBuilder.Entity<RoomDatum>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
