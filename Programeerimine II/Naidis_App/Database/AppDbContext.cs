﻿using Microsoft.EntityFrameworkCore;

namespace Naidis_App
{
    public class AppDbContext : DbContext
    {
        public DbSet<Kontakt> Kontakt { get; set; }
        public DbSet<Riik> Riik { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "naidisapp.db");
                optionsBuilder.UseSqlite($"Filename={dbPath}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Kontakt>().HasData(
                new Kontakt { Id = 1, Nimi = "Bogdan Viblyy", EmailVoiTelefon = "vladislav.kudriashev@gmail.com" },
                new Kontakt { Id = 2, Nimi = "Mikhail Agapov", EmailVoiTelefon = "vladislav.kudriashev@gmail.com" },
                new Kontakt { Id = 3, Nimi = "Donald Trump", EmailVoiTelefon = "vladislav.kudriashev@gmail.com" },
                new Kontakt { Id = 4, Nimi = "Toomas Sularaha", EmailVoiTelefon = "vladislav.kudriashev@gmail.com" }
            );
            modelBuilder.Entity<Riik>().HasData(
                new Riik { Id = 1, Nimi = "Eesti", Pealinn = "Tallinn", Rahvastik = 1369285, Lipp = "eesti.svg", Keel = "Eesti" },
                new Riik { Id = 2, Nimi = "Soome", Pealinn = "Helsingi", Rahvastik = 5635724, Lipp = "soome.svg", Keel = "Soome" },
                new Riik { Id = 3, Nimi = "Venemaa", Pealinn = "Moskva", Rahvastik = 146028325, Lipp = "venemaa.svg", Keel = "Vene" },
                new Riik { Id = 4, Nimi = "Indoneesia", Pealinn = "Jakarta", Rahvastik = 282477584, Lipp = "indoneesia.svg", Keel = "Indoneesia" },
                new Riik { Id = 5, Nimi = "Etioopia", Pealinn = "Addis Abeba", Rahvastik = 132900000, Lipp = "etioopia.svg", Keel = "Amhara" }
            );
        }
    }
}