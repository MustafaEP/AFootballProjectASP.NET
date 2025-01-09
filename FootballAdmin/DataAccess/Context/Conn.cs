using Entities.Concrete;
using Entities.Concrete.AddedFootballersExplorer;
using Entities.Concrete.ClubFormation;
using Entities.Concrete.FootballerExplorer;
using Entities.Concrete.FootballerStatistics;
using Entities.Concrete.FootballerStatistics.Statistics;
using Entities.Concrete.Matches;
using Entities.Concrete.NewVersion;
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
        public DbSet<AdminNotification> AdminNotifications { get; set; }
        public DbSet<ManagerClub> ManagerClubs { get; set; }
        public DbSet<Footballer> Footballers { get; set; }
        public DbSet<ClubsFootballer> ClubsFootballers { get; set; }
        public DbSet<ScoreofFootballer> ScoreofFootballers { get; set; }
        public DbSet<FootballMatch> FootballMatches { get; set; }



        public DbSet<FootballerStatistic> FootballerStatistics { get; set; }
        public DbSet<FootballerData> FootballerDatas { get; set; }
        public DbSet<FootballerPosition> FootballerPositions { get; set; }
        public DbSet<FootballerAbility> FootballerAbilities { get; set; }
        public DbSet<FootballerDefending> FootballerDefendings { get; set; }
        public DbSet<FootballerDribbling> FootballerDribblings { get; set; }
        public DbSet<FootballerPace> FootballerPaces { get; set; }
        public DbSet<FootballerPassing> FootballerPassings { get; set; }
        public DbSet<FootballerPhysicality> footballerPhysicalities { get; set; }
        public DbSet<FootballerShooting> FootballerShootings { get; set; }


        public DbSet<Formation> Formations { get; set; }
        public DbSet<XIPlayer> XIPlayers { get; set; }
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

            modelBuilder.Entity<FootballMatch>(entity =>
            {
                entity.HasKey(f => f.Id);

                entity.HasOne(f => f.HomeClub)
                      .WithMany()
                      .HasForeignKey(f => f.HomeClubId)
                      .OnDelete(DeleteBehavior.Restrict); // Cascade yerine Restrict

                entity.HasOne(f => f.AwayClub)
                      .WithMany()
                      .HasForeignKey(f => f.AwayClubId)
                      .OnDelete(DeleteBehavior.Restrict); // Cascade yerine Restrict
            });


            //FootballerStatics
            modelBuilder.Entity<Footballer>()
                .HasOne(f => f.Statistic)
                .WithOne(s => s.Footballer)
                .HasForeignKey<FootballerStatistic>(s => s.FootballerId);

            modelBuilder.Entity<FootballerStatistic>()
                .HasMany(f => f.footballerAbilities)
                .WithOne(s => s.FootballerStatistic)
                .HasForeignKey(sa => sa.FootballerStatisticId);

            modelBuilder.Entity<Footballer>()
                .HasMany(f => f.FootballerPositions)
                .WithOne(s => s.Footballer)
                .HasForeignKey(sa => sa.FootballerId);

            modelBuilder.Entity<FootballerStatistic>()
                 .HasOne(fs => fs.FootballerDatas)
                 .WithOne(b => b.FootballerStatistic)
                 .HasForeignKey<FootballerData>(b => b.StatisticsId);
            
            modelBuilder.Entity<FootballerStatistic>()
                 .HasOne(fs => fs.DribblingDetails)
                 .WithOne(b => b.FootballerStatistic)
                 .HasForeignKey<FootballerDribbling>(b => b.StatisticsId);

            modelBuilder.Entity<FootballerStatistic>()
                .HasOne(fs => fs.DefendingDetails)
                .WithOne(b => b.FootballerStatistic)
                .HasForeignKey<FootballerDefending>(b => b.StatisticsId);

            modelBuilder.Entity<FootballerStatistic>()
                .HasOne(f => f.PaceDetails)
                .WithOne(s => s.FootballerStatistic)
                .HasForeignKey<FootballerPace>(s => s.StatisticsId);

            modelBuilder.Entity<FootballerStatistic>()
                .HasOne(f => f.PassingDetails)
                .WithOne(s => s.FootballerStatistic)
                .HasForeignKey<FootballerPassing>(s => s.StatisticsId);

            modelBuilder.Entity<FootballerStatistic>()
                .HasOne(f => f.PhysicalityDetails)
                .WithOne(s => s.FootballerStatistic)
                .HasForeignKey<FootballerPhysicality>(s => s.StatisticsId);

            modelBuilder.Entity<FootballerStatistic>()
                .HasOne(f => f.ShootingDetails)
                .WithOne(s => s.FootballerStatistic)
                .HasForeignKey<FootballerShooting>(s => s.StatisticsId);

            modelBuilder.Entity<AddedFootballer>()
                .HasOne(af => af.Footballer)
                .WithMany(f => f.AddedFootballers)  // Eğer Footballer birden fazla AddedFootballer'a sahip olacaksa
                .HasForeignKey(af => af.FootballerId)
                .OnDelete(DeleteBehavior.Cascade);

            // AddedFootballer ve ManagerClub arasındaki ilişki
            modelBuilder.Entity<AddedFootballer>()
                .HasOne(af => af.Club)
                .WithMany(c => c.AddedFootballers)
                .HasForeignKey(af => af.ClubId)
                .OnDelete(DeleteBehavior.Cascade);

            // AddedFootballer ve AddedFootballerUpgrade arasındaki ilişki (Bire bir ilişki)
            modelBuilder.Entity<AddedFootballer>()
                .HasOne(af => af.Upgrade)
                .WithOne(afu => afu.AddedFootballer)
                .HasForeignKey<AddedFootballerUpgrade>(afu => afu.AddedFootballerId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<ManagerClub>()
                .HasOne(mc => mc.Formation)
                .WithOne(f => f.ManagerClub)
                .HasForeignKey<Formation>(fo => fo.ManagerClubId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<XIPlayer>()
                .HasOne(xi => xi.Formation)
                .WithMany(f => f.XIPlayers)
                .HasForeignKey(xi => xi.FormationId)
                .OnDelete(DeleteBehavior.NoAction); // Cascade yerine NoAction

            modelBuilder.Entity<XIPlayer>()
                .HasOne(xi => xi.AddedFootballer)
                .WithMany()
                .HasForeignKey(xi => xi.AddedFootballerId)
                .OnDelete(DeleteBehavior.Cascade); // Yalnızca birine Cascade uygulanabilir




        }
    }
}
