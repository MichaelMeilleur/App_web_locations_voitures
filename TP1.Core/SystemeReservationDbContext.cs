using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP1.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using TP1.Core.Domain.Entities.Locations;
using TP1.Core.Data;
using TP1.Core.Domain.Entities.Identité;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace TP1.Core
{
    public class SystemeReservationDbContext : IdentityDbContext<Utilisateur, IdentityRole<Guid>, Guid>

    {
        public DbSet<Succursale> Succursales { get; set; }
        public DbSet<Adresse> Adresses { get; set; }
        public DbSet<Voiture> Voitures { get; set; }
        public DbSet<Conducteur> Conducteurs { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Note> Note { get; set; }

        public SystemeReservationDbContext(DbContextOptions
                    <SystemeReservationDbContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Conducteur>()
            .HasOne(c => c.Voiture)
            .WithMany()
            .HasForeignKey(c => c.VoitureId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Seed();
        }
    }
}