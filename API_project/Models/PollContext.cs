using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_project.Models
{
    public class PollContext : DbContext
    {
        public PollContext(DbContextOptions<PollContext> options) : base(options)
        { }

        public DbSet<Gebruiker> Gebruikers { get; set; }
        public DbSet<Poll> Polls { get; set; }
        public DbSet<PollGebruiker> PollGebruikers { get; set; }
        public DbSet<Stem> Stemmen { get; set; }
        public DbSet<Optie> Opties { get; set; }
        public DbSet<Vriend> Vrienden { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gebruiker>().ToTable("Gebruiker");

            modelBuilder.Entity<Poll>().ToTable("Poll")
                .HasMany(o => o.Opties)
                .WithOne(p => p.Poll)
                .HasForeignKey(p => p.PollID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PollGebruiker>().ToTable("PollGebruiker")
                .HasOne(u => u.Gebruiker)
                .WithMany(u => u.PollGebruikers)
                .HasForeignKey(u => u.GebruikerID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PollGebruiker>().ToTable("PollGebruiker")
                .HasOne(u => u.Poll)
                .WithMany(u => u.Deelnemers)
                .HasForeignKey(u => u.PollID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Stem>().ToTable("Stem");

            modelBuilder.Entity<Optie>().ToTable("Optie")
                .HasMany(o => o.Stemmen)
                .WithOne(o => o.Optie)
                .HasForeignKey(o => o.OptieID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Vriend>().ToTable("Vriend")
                .HasOne(v => v.GebruikerFrom)
                .WithMany(v => v.Verzonden)
                .HasForeignKey(v => v.friendFrom)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Vriend>().ToTable("Vriend")
               .HasOne(v => v.GebruikerTo)
               .WithMany(v => v.Gekregen)
               .HasForeignKey(v => v.friendTo)
               .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
