using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SignalRwithEntityFramework.Models
{
    public partial class SignalRWithEFContext : DbContext
    {
        public SignalRWithEFContext()
        {
        }

        public SignalRWithEFContext(DbContextOptions<SignalRWithEFContext> options)
            : base(options)
        {
        }

        public virtual DbSet<HubConnection> HubConnections { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HubConnection>(entity =>
            {
                entity.ToTable("HubConnection");

                entity.Property(e => e.ConnectionId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("Notification");

                entity.Property(e => e.Message)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.MessageType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NotificationDateTime).HasColumnType("datetime");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
