using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.IO;

namespace FootballAnalyst.Models
{
    public partial class dataContext : DbContext
    {
        public dataContext()
        {
        }

        public dataContext(DbContextOptions<dataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<Match> Matches { get; set; } = null!;
        public virtual DbSet<Player> Players { get; set; } = null!;
        public virtual DbSet<Season> Seasons { get; set; } = null!;
        public virtual DbSet<Team> Teams { get; set; } = null!;
        public virtual DbSet<Tournament> Tournaments { get; set; } = null!;
        private string DbPath = @"Assets\data.db";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string directoryPath = Directory.GetCurrentDirectory();
                directoryPath = directoryPath.Remove(directoryPath.LastIndexOf("bin"));
                DbPath = directoryPath + DbPath;
                optionsBuilder.UseSqlite("Data Source=" + DbPath);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("countries");

                entity.HasIndex(e => e.Flag, "IX_countries_flag")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Flag).HasColumnName("flag");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<Match>(entity =>
            {
                entity.ToTable("matches");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.AwayTeamGoals).HasColumnName("away_team_goals");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.HomeTeamGoals).HasColumnName("home_team_goals");

                entity.Property(e => e.IdAwayTeam).HasColumnName("id_away_team");

                entity.Property(e => e.IdHomeTeam).HasColumnName("id_home_team");

                entity.Property(e => e.IdSeason).HasColumnName("id_season");

                entity.Property(e => e.IdTournament).HasColumnName("id_tournament");

                entity.Property(e => e.Referee).HasColumnName("referee");

                entity.Property(e => e.Time).HasColumnName("time");

                entity.Property(e => e.Venue).HasColumnName("venue");

                entity.HasOne(d => d.IdAwayTeamNavigation)
                    .WithMany(p => p.MatchIdAwayTeamNavigations)
                    .HasForeignKey(d => d.IdAwayTeam)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.IdHomeTeamNavigation)
                    .WithMany(p => p.MatchIdHomeTeamNavigations)
                    .HasForeignKey(d => d.IdHomeTeam)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.ToTable("players");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FullName).HasColumnName("full_name");

                entity.Property(e => e.IdCountry).HasColumnName("id_country");

                entity.Property(e => e.IdTeam).HasColumnName("id_team");

                entity.HasOne(d => d.IdCountryNavigation)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.IdCountry);
            });

            modelBuilder.Entity<Season>(entity =>
            {
                entity.ToTable("seasons");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.ToTable("teams");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.IdCountry).HasColumnName("id_country");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.HasOne(d => d.IdCountryNavigation)
                    .WithMany(p => p.Teams)
                    .HasForeignKey(d => d.IdCountry);
            });

            modelBuilder.Entity<Tournament>(entity =>
            {
                entity.HasKey(e => new { e.IdTournament, e.IdSeason });

                entity.ToTable("tournaments");

                entity.Property(e => e.IdTournament).HasColumnName("id_tournament");

                entity.Property(e => e.IdSeason).HasColumnName("id_season");

                entity.Property(e => e.IdChampion).HasColumnName("id_champion");

                entity.Property(e => e.IdCountry).HasColumnName("id_country");

                entity.Property(e => e.IdTopPlayer).HasColumnName("id_top_player");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.HasOne(d => d.IdChampionNavigation)
                    .WithMany(p => p.Tournaments)
                    .HasForeignKey(d => d.IdChampion);

                entity.HasOne(d => d.IdCountryNavigation)
                    .WithMany(p => p.Tournaments)
                    .HasForeignKey(d => d.IdCountry);

                entity.HasOne(d => d.IdSeasonNavigation)
                    .WithMany(p => p.Tournaments)
                    .HasForeignKey(d => d.IdSeason)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.IdTopPlayerNavigation)
                    .WithMany(p => p.Tournaments)
                    .HasForeignKey(d => d.IdTopPlayer);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
