using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Access.Context
{
    public class Conn : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // appsettings.json dosyasını okuma
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Bağlantı stringini okuma ve yapılandırma
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("FootballAdminConnection"));
        }


        public DbSet<Admin> Admins { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }

        public DbSet<Match> Matches { get; set; }
        public DbSet<TrainingSession> TrainingSessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // HomeTeam ve AwayTeam ilişkileri
            modelBuilder.Entity<Match>()
                .HasOne(m => m.HomeTeam)
                .WithMany(t => t.HomeMatches)
                .HasForeignKey(m => m.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.AwayTeam)
                .WithMany(t => t.AwayMatches)
                .HasForeignKey(m => m.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            // Manager ve Team birebir ilişkisi
            modelBuilder.Entity<Team>()
                .HasOne(t => t.Manager)
                .WithOne(m => m.Team)
                .HasForeignKey<Team>(t => t.ManagerId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Player>()
               .HasOne(p => p.Team)
               .WithMany(t => t.ListPlayer)  // Takımın oyuncuları
               .HasForeignKey(p => p.TeamId)  // Yabancı anahtar
               .OnDelete(DeleteBehavior.SetNull);  // Takım silindiğinde oyuncunun TeamId'si null olur
        }
    }
}
