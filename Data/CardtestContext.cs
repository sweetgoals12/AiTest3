using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using aitest3.Models;

namespace aitest3.Data;

public partial class CardtestContext : IdentityDbContext
{
    public CardtestContext()
    {
    }

    public CardtestContext(DbContextOptions<CardtestContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Card> Cards { get; set; }

    public virtual DbSet<CardSet> CardSets { get; set; }

    public virtual DbSet<NhlupperDeckMvp20212022> NhlupperDeckMvp20212022s { get; set; }

    public virtual DbSet<NhlupperDeckMvp20252026> NhlupperDeckMvp20252026s { get; set; }

    public virtual DbSet<UserCard> UserCards { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=cardtest;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.CardSets).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserCardSet",
                    r => r.HasOne<CardSet>().WithMany().HasForeignKey("CardSetId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "CardSetId");
                        j.ToTable("UserCardSets");
                        j.HasIndex(new[] { "CardSetId" }, "IX_UserCardSets_CardSetId");
                    });

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Card>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.CardNumber).HasMaxLength(50);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.PlayerName).HasMaxLength(150);
            entity.Property(e => e.SetName).HasMaxLength(100);
            entity.Property(e => e.TeamCity).HasMaxLength(100);
            entity.Property(e => e.TeamName).HasMaxLength(100);
        });

        modelBuilder.Entity<NhlupperDeckMvp20212022>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UpperDec__3214EC0797B752EA");

            entity.ToTable("NHLUpperDeckMvp2021_2022");

            entity.Property(e => e.CardNumber).HasMaxLength(50);
            entity.Property(e => e.PlayerName).HasMaxLength(150);
            entity.Property(e => e.SetName).HasMaxLength(100);
            entity.Property(e => e.TeamCity).HasMaxLength(100);
            entity.Property(e => e.TeamName).HasMaxLength(100);
        });

        modelBuilder.Entity<NhlupperDeckMvp20252026>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NHLUpper__3214EC07CF5027F9");

            entity.ToTable("NHLUpperDeckMvp2025_2026");

            entity.Property(e => e.CardNumber).HasMaxLength(50);
            entity.Property(e => e.Odds).HasMaxLength(50);
            entity.Property(e => e.PlayerName).HasMaxLength(255);
            entity.Property(e => e.Points).HasMaxLength(50);
            entity.Property(e => e.Rookie).HasDefaultValue(false);
            entity.Property(e => e.SetName).HasMaxLength(100);
            entity.Property(e => e.TeamCity).HasMaxLength(100);
            entity.Property(e => e.TeamName).HasMaxLength(100);
        });

        modelBuilder.Entity<UserCard>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_UserCard");

            entity.HasIndex(e => e.CardSetId, "IX_UserCard_CardSetId");

            entity.HasIndex(e => e.UserId, "IX_UserCard_UserId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
